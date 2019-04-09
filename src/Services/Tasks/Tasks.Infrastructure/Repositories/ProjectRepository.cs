using System.Threading.Tasks;
using Tasks.Domain.AggregatesModel.ProjectsAggregate;
using Tasks.Domain.SeedWork;

namespace Tasks.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TasksContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProjectRepository(TasksContext context)
        {
            _context = context;
        }

        public Project Add(Project project)
        {
            return _context.Projects.Add(project).Entity;
        }

        public Project Update(Project project)
        {
            return _context.Projects.Update(project).Entity;
        }

        public void Remove(Project project)
        {
            _context.Projects.Remove(project);
        }

        public async Task<Project> GetAsync(int projectId)
        {
            return await _context.Projects.FindAsync(projectId);
        }
    }
}
