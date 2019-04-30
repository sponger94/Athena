using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athena.BuildingBlocks.EventBus.Events;

namespace Tasks.API.Application.IntegrationEvents
{
    public interface IUserTasksIntegrationEventService
    {
        Task AddAndSaveEventAsync();
        Task PublishEventsThroughEventBusAsync(IntegrationEvent evt);
    }
}
