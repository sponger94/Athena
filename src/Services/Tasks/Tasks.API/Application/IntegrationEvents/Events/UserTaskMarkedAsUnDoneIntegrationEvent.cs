using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athena.BuildingBlocks.EventBus.Events;

namespace Tasks.API.Application.IntegrationEvents.Events
{
    public class UserTaskMarkedAsUnDoneIntegrationEvent : IntegrationEvent
    {
        public int UserTaskId { get; }
        public string UserTaskName { get; }

        public UserTaskMarkedAsUnDoneIntegrationEvent(int userTaskId, string userTaskName)
        {
            UserTaskId = userTaskId;
            UserTaskName = userTaskName;
        }
    }
}
