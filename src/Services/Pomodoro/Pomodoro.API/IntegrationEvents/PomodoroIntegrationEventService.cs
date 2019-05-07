using System;
using System.Data.Common;
using System.Threading.Tasks;
using Athena.BuildingBlocks.EventBus.Abstractions;
using Athena.BuildingBlocks.EventBus.Events;
using Athena.BuildingBlocks.IntegrationEventLogEF;
using Athena.BuildingBlocks.IntegrationEventLogEF.Services;
using Athena.BuildingBlocks.IntegrationEventLogEF.Utilities;
using Athena.Pomodoros.API.Infrastructure;
using Athena.Pomodoros.API.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Tasks.API.Application.IntegrationEvents
{
    public class PomodoroIntegrationEventService : IPomodoroIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly PomodoroContext _pomodoroContext;
        private readonly IntegrationEventLogContext _eventLogContext;
        private readonly IIntegrationEventLogService _eventLogService;

        public PomodoroIntegrationEventService(Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory, 
            IEventBus eventBus, 
            PomodoroContext pomodoroContext, 
            IntegrationEventLogContext eventLogContext)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _pomodoroContext = pomodoroContext ?? throw new ArgumentNullException(nameof(pomodoroContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventLogContext = eventLogContext ?? throw new ArgumentNullException(nameof(eventLogContext));
            _eventLogService = _integrationEventLogServiceFactory(_pomodoroContext.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                _eventBus.Publish(evt);
                await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception)
            {
                await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }            
        }

        public async Task SaveEventAndPomodoroContextChangesAsync(IntegrationEvent evt)
        {
            await ResilientTransaction.New(_pomodoroContext)
                .ExecuteAsync(async () =>
                {
                    await _pomodoroContext.SaveChangesAsync();
                    await _eventLogService.SaveEventAsync(evt,
                        _pomodoroContext.Database.CurrentTransaction.GetDbTransaction());
                });
        }
    }
}
