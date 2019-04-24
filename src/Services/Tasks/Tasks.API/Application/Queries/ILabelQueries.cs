using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tasks.API.Application.Queries
{
    public interface ILabelQueries
    {
        Task<Label> GetLabelByIdAsync(int labelId);

        Task<IEnumerable<Label>> GetLabelsAsync();
    }
}
