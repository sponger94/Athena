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
    public class TasksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly IUserTaskQueries _taskQueries;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTask>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserTask>>> GetTasksAsync()
        {
            var userId = _identityService.GetUserIdentity();
            var userTasks = _taskQueries.GetTasksFromUserAsync(Guid.Parse(userId));

            return Ok(userTasks);
        }
    }
}