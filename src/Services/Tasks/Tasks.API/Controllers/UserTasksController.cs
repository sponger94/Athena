using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.API.Application.Queries;
using Tasks.API.Services;

namespace Tasks.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class UserTasksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly IUserTaskQueries _taskQueries;

        public UserTasksController(IMediator mediator, IIdentityService identityService, IUserTaskQueries taskQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _taskQueries = taskQueries ?? throw new ArgumentNullException(nameof(taskQueries));
        }

        [Route("{userTaskId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(UserTask), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserTaskByIdAsync(int userTaskId)
        {
            try
            {
                var userTask = await _taskQueries.GetTaskAsync(userTaskId);
                return Ok(userTask);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTaskSummary>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserTaskSummary>>> GetUserTasksAsync()
        {
            var userId = _identityService.GetUserIdentity();
            var userTasks = await _taskQueries.GetTasksFromUserAsync(Guid.Parse(userId), 20, 0);

            return Ok(userTasks);
        }
    }
}