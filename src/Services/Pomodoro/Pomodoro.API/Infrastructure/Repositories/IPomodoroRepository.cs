using Athena.Pomodoros.API.Model;
using Athena.Pomodoros.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Athena.Pomodoros.API.Infrastructure.Repositories
{
    public interface IPomodoroRepository
    {
        Task<Pomodoro> AddAsync(Pomodoro pomodoro);
        Task<Pomodoro> GetItemByIdAsync(int id);
        Task<IEnumerable<Pomodoro>> GetItemsByIdsAsync(string ids);
        Task<PaginatedItemsViewModel<Pomodoro>> GetPomodoroItemsAsync(int pageIndex, int pageSize);
        Task RemoveAsync(Pomodoro pomodoro);
    }
}
