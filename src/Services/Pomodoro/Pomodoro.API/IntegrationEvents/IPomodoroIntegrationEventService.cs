using System.Threading.Tasks;
using Athena.BuildingBlocks.EventBus.Events;

namespace Athena.Pomodoros.API.IntegrationEvents
{
    public interface IPomodoroIntegrationEventService
    {
        Task SaveEventAndPomodoroContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
