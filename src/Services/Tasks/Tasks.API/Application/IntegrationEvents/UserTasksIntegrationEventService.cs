using System;
using System.Data.Common;
using System.Threading.Tasks;
using Athena.BuildingBlocks.EventBus.Abstractions;
using Athena.BuildingBlocks.EventBus.Events;
using Athena.BuildingBlocks.IntegrationEventLogEF;
using Athena.BuildingBlocks.IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tasks.Infrastructure;

namespace Tasks.API.Application.IntegrationEvents
{
    public class UserTasksIntegrationEventService : IUserTasksIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly TasksContext _tasksContext;
        private readonly IntegrationEventLogContext _eventLogContext;
        private readonly IIntegrationEventLogService _eventLogService;

        public UserTasksIntegrationEventService(Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory, 
            IEventBus eventBus, 
            TasksContext tasksContext, 
            IntegrationEventLogContext eventLogContext)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _tasksContext = tasksContext ?? throw new ArgumentNullException(nameof(tasksContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventLogContext = eventLogContext ?? throw new ArgumentNullException(nameof(eventLogContext));
            _eventLogService = _integrationEventLogServiceFactory(_tasksContext.Database.GetDbConnection());
        }

        public async Task AddAndSaveEventAsync()
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync();
            foreach (var logEvent in pendingLogEvents)
            {
                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvent.EventId);
                    _eventBus.Publish(logEvent.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvent.EventId);
                }
                catch (Exception )
                {
                    await _eventLogService.MarkEventAsFailedAsync(logEvent.EventId);
                }
            }
        }

        public async Task PublishEventsThroughEventBusAsync(IntegrationEvent evt)
        {
            await _eventLogService.SaveEventAsync(evt, _tasksContext.CurrentTransaction.GetDbTransaction());
        }
    }
}
