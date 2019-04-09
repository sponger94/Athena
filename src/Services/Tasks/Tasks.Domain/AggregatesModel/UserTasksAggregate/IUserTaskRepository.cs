using System.Threading.Tasks;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.UserTasksAggregate
{
    public interface IUserTaskRepository : IRepository<UserTask>
    {
        UserTask Add(UserTask task);
        UserTask Update(UserTask task);
        void Remove(UserTask task);
        Task<UserTask> GetAsync(int taskId);
    }
}
