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

        //GET api/v1/[controller]/items/4
        [HttpGet]
        [Route("items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Model.Pomodoro), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Model.Pomodoro>> ItemByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            var item = await _pomodoroContext.Pomodoros.SingleOrDefaultAsync(p => p.Id == id);

            if (item != null)
            {
                return item;
            }

            return NotFound();
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

        //POST api/v1/[controller]/create
        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> FinishPomodoroAsync([FromBody] Model.Pomodoro pomodoro)
        {
            var pomodoroItem = new Model.Pomodoro
            {
                ProjectId = pomodoro.ProjectId,
                ProjectName = pomodoro.ProjectName,
                Duration = pomodoro.Duration,
                Time = pomodoro.Time,
                UserId = pomodoro.UserId
            };
            _pomodoroContext.Add(pomodoro);
            await _pomodoroContext.SaveChangesAsync();

            return CreatedAtAction(nameof(ItemByIdAsync), new { id = pomodoroItem.Id }, null);
        }

        //DELETE api/v1/[controller]/delete/id
        [HttpDelete]
        [Route("delete/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletePomodoroAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var pomodoroItem = await _pomodoroContext.Pomodoros.SingleOrDefaultAsync(p => p.Id == id);
            if (pomodoroItem == null)
            {
                return NotFound();
            }

            _pomodoroContext.Pomodoros.Remove(pomodoroItem);
            await _pomodoroContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
