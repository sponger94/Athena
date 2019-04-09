using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.ProjectsAggregate
{
    public interface IProjectRepository : IRepository<Project>
    {
        Project Add(Project project);
        Project Update(Project project);
        void Remove(Project project);
        Task<Project> GetAsync(int projectId);
    }
}
