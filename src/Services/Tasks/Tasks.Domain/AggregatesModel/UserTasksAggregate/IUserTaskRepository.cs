using System.Threading.Tasks;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.UserTasksAggregate
{
    public interface IUserTaskRepository : IRepository<UserTask>
    {
        UserTask Add(UserTask userTask);
        UserTask Update(UserTask userTask);
        void Remove(UserTask userTask);
        Task<UserTask> GetAsync(int userTaskId);
    }
}
