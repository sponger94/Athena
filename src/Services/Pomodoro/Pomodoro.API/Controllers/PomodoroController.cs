using Athena.Pomodoro.API.Infrastructure;
using Athena.Pomodoro.API.IntegrationEvents;
using Athena.Pomodoro.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Athena.Pomodoro.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PomodoroController : ControllerBase
    {
        private readonly PomodoroContext _pomodoroContext;
        private readonly PomodoroSettings _settings;
        private readonly IPomodoroIntegrationEventService _pomodoroIntegrationEventService;

        public PomodoroController(PomodoroContext context,
            IOptionsSnapshot<PomodoroSettings> settings,
            IPomodoroIntegrationEventService pomodoroIntegrationEventService)
        {
            _pomodoroContext = context ?? throw new ArgumentNullException(nameof(context));
            _pomodoroIntegrationEventService = pomodoroIntegrationEventService ?? throw new ArgumentNullException(nameof(settings));
            _settings = settings.Value;

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //GET api/v1/[controller]/items[?pageSize=7&pageIndex=21]
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Model.Pomodoro>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Model.Pomodoro>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0,
            string ids = null)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = await GetItemsByIdsAsync(ids);

                if (!items.Any())
                {
                    return BadRequest("Ids value invalid. Must be comma-separated list of numbers.");
                }

                return Ok(items);
            }

            var totalItems = await _pomodoroContext.Pomodoros
                .LongCountAsync();

            var itemsOnPage = await _pomodoroContext.Pomodoros
                .OrderBy(p => p.Time)
                .Skip(pageSize * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var model = new PaginatedItemsViewModel<Model.Pomodoro>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        private async Task<List<Model.Pomodoro>> GetItemsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.Any(nid => nid.Ok))
            {
                return new List<Model.Pomodoro>();
            }

            var idsToSelect = numIds.Select(id => id.Value);

            return await _pomodoroContext.Pomodoros.Where(p => idsToSelect.Contains(p.Id)).ToListAsync();
        }
    }
}
