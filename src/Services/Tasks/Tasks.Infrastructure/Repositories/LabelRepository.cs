using System.Threading.Tasks;
using Tasks.Domain.AggregatesModel.LabelsAggregate;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;
using Tasks.Domain.SeedWork;

namespace Tasks.Infrastructure.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly TasksContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public LabelRepository(TasksContext context)
        {
            _context = context;
        }

        public Label Add(Label label)
        {
            return _context.Labels.Add(label).Entity;
        }

        public async Task<Label> GetAsync(int labelId)
        {
            return await _context.Labels.FindAsync(labelId);
        }

        public void Remove(Label label)
        {
            _context.Labels.Remove(label);
        }

        public Label Update(Label label)
        {
            return _context.Labels.Update(label).Entity;
        }
    }
}
