using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tasks.API.Application.Commands;
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

        [HttpGet]
        [Route("{userTaskId:int}")]
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

        //GET api/v1/[controller][?pageSize=3&pageIndex=18]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTaskSummary>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserTaskSummary>>> GetUserTasksAsync([FromQuery]int pageSize = 20, [FromQuery]int pageIndex = 0)
        {
            var userId = _identityService.GetUserIdentity();
            var userTasks = await _taskQueries.GetUserTasksAsync(Guid.Parse(userId), 20, 0);

            return Ok(userTasks);
        }

        //GET api/v1/[controller]/projects[?pageSize=3&pageIndex=18]
        [HttpGet]
        [Route("projects")]
        [ProducesResponseType(typeof(IEnumerable<ProjectSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProjectsAsync([FromQuery] int pageSize = 20, [FromQuery] int pageIndex = 0)
        {
            var userId = _identityService.GetUserIdentity();
            var projectUserTasks = await _taskQueries.GetProjectsAsync(Guid.Parse(userId), pageSize, pageIndex);

            return Ok(projectUserTasks);
        }

        //GET api/v1/[controller]/projects/5[?pageSize=3&pageIndex=18]
        [HttpGet]
        [Route("projects/{projectId:int}")]
        [ProducesResponseType(typeof(IEnumerable<UserTaskSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProjectUserTasksAsync(int projectId, [FromQuery]int pageSize = 20, [FromQuery]int pageIndex = 0)
        {
            var userId = _identityService.GetUserIdentity();
            var projectUserTasks = await _taskQueries.GetProjectUserTasksAsync(Guid.Parse(userId), projectId, pageSize, pageIndex);

            return Ok(projectUserTasks);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUserTask([FromBody] CreateUserTaskCommand createUserTaskCommand)
        {
            bool result = await _mediator.Send(createUserTaskCommand);

            if (result)
                return Ok();

            return BadRequest();
        }
    }
}