using Athena.BuildingBlocks.EventBus.Events;

namespace Tasks.API.Application.IntegrationEvents.Events
{
    public class UserTaskMarkedAsDoneIntegrationEvent : IntegrationEvent
    {
        public int UserTaskId { get; }
        public string UserTaskName { get; }

        public UserTaskMarkedAsDoneIntegrationEvent(int userTaskId, string userTaskName)
        {
            UserTaskId = userTaskId;
            UserTaskName = userTaskName;
        }
    }
}
