using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.API.Application.Queries
{
    public interface IUserTaskQueries
    {
        Task<UserTask> GetTaskAsync(int userTaskId);

        Task<IEnumerable<UserTaskSummary>> GetUserTasksAsync(Guid userId, int pageSize, int pageIndex);

        Task<IEnumerable<ProjectSummary>> GetProjectsAsync(Guid userId, int pageSize, int pageIndex);

        Task<IEnumerable<UserTaskSummary>> GetProjectUserTasksAsync(Guid userId, int projectId, int pageSize, int pageIndex);
    }
}
