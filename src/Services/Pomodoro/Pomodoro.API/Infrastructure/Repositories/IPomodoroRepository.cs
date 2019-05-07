using Athena.Pomodoro.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Athena.Pomodoro.API.Infrastructure.Repositories
{
    public interface IPomodoroRepository
    {
        Task<Model.Pomodoro> AddAsync(Model.Pomodoro pomodoro);
        Task<Model.Pomodoro> GetItemByIdAsync(int id);
        Task<IEnumerable<Model.Pomodoro>> GetItemsByIdsAsync(string ids);
        Task<PaginatedItemsViewModel<Model.Pomodoro>> GetPomodoroItemsAsync(int pageIndex, int pageSize);
        Task RemoveAsync(Model.Pomodoro pomodoro);
    }
}
