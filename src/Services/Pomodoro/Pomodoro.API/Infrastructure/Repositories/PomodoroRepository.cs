using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athena.Pomodoros.API.Model;
using Athena.Pomodoros.API.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Athena.Pomodoros.API.Infrastructure.Repositories
{
    public class PomodoroRepository : IPomodoroRepository
    {
        private readonly PomodoroContext _pomodoroContext;

        public PomodoroRepository(PomodoroContext pomodoroContext)
        {
            _pomodoroContext = pomodoroContext;
            _pomodoroContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Model.Pomodoro> AddAsync(Model.Pomodoro pomodoro)
        {
            await _pomodoroContext.Pomodoros.AddAsync(pomodoro);
            await _pomodoroContext.SaveChangesAsync();
            return pomodoro;
        }

        public async Task<Model.Pomodoro> GetItemByIdAsync(int id)
        {
            return await _pomodoroContext.Pomodoros.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Model.Pomodoro>> GetItemsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.Any(nid => nid.Ok))
            {
                return new List<Model.Pomodoro>();
            }

            var idsToSelect = numIds.Select(id => id.Value);

            return await _pomodoroContext.Pomodoros.Where(p => idsToSelect.Contains(p.Id)).ToListAsync();
        }

        public async Task<PaginatedItemsViewModel<Model.Pomodoro>> GetPomodoroItemsAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _pomodoroContext.Pomodoros
                .LongCountAsync();

            var itemsOnPage = await _pomodoroContext.Pomodoros
                .OrderBy(p => p.Time)
                .Skip(pageSize * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<Model.Pomodoro>(pageIndex, pageSize, totalItems, itemsOnPage);
        }

        public async Task RemoveAsync(Model.Pomodoro pomodoro)
        {
            _pomodoroContext.Pomodoros.Remove(pomodoro);
            await _pomodoroContext.SaveChangesAsync();
        }
    }
}
