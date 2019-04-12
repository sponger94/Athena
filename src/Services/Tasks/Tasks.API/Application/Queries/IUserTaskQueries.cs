using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.API.Application.Queries
{
    public interface IUserTaskQueries
    {
        Task<UserTask> GetTaskAsync(int id);

        Task<IEnumerable<UserTask>> GetTasksFromUserAsync(Guid userId);
    }
}
