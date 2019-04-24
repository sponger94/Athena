using System.Threading.Tasks;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.LabelsAggregate
{
    public interface ILabelRepository : IRepository<Label>
    {
        Label Add(Label label);
        Label Update(Label label);
        void Remove(Label label);
        Task<Label> GetAsync(int labelId);
    }
}
