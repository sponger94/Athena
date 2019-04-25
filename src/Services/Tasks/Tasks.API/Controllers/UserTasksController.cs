using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tasks.API.Application.Commands;
using Tasks.API.Application.Commands.UserTask;
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

        //POST api/v1/[controller]/create
        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUserTaskAsync([FromBody] CreateUserTaskCommand createUserTaskCommand)
        {
            bool result = await _mediator.Send(createUserTaskCommand);

            if (result)
                return Ok();

            return BadRequest();
        }

        //DELETE api/v1/[controller]/delete
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUserTaskAsync([FromBody] RemoveUserTaskCommand removeUserTaskCommand)
        {
            bool result = await _mediator.Send(removeUserTaskCommand);

            if (result)
                return Ok();

            return BadRequest();
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("add/attachment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddAttachmentAsync([FromBody] AddAttachmentCommand addAttachmentCommand)
        {
            bool result = await _mediator.Send(addAttachmentCommand);

            if (result)
                return Ok();

            return BadRequest();
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("remove/attachment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveAttachmentAsync([FromBody] RemoveAttachmentCommand removeAttachmentCommand)
        {
            bool result = await _mediator.Send(removeAttachmentCommand);

            if (result)
                return Ok();

            return BadRequest();
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("add/label")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddLabelItemAsync([FromBody] AddLabelItemCommand addLabelCommand)
        {
            bool result = await _mediator.Send(addLabelCommand);

            if (result)
                return Ok();

            return BadRequest();
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("remove/label")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveLabelItemAsync([FromBody] RemoveLabelItemCommand removeLabelCommand)
        {
            bool result = await _mediator.Send(removeLabelCommand);

            if (result)
                return Ok();

            return BadRequest();
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("add/note")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddNoteAsync([FromBody] AddNoteCommand addNoteCommand)
        {
            bool result = await _mediator.Send(addNoteCommand);

            if (result)
                return Ok();

            return BadRequest();
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("remove/note")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveNoteAsync([FromBody] RemoveNoteCommand removeNoteCommand)
        {
            bool result = await _mediator.Send(removeNoteCommand);

            if (result)
                return Ok();

            return BadRequest();
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("add/subtask")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddSubTaskAsync([FromBody] AddSubTaskCommand addSubTaskCommand)
        {
            bool result = await _mediator.Send(addSubTaskCommand);

            if (result)
                return Ok();

            return BadRequest();
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("remove/subtask")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveSubTaskAsync([FromBody] RemoveSubTaskCommand removeSubTaskCommand)
        {
            bool result = await _mediator.Send(removeSubTaskCommand);

            if (result)
                return Ok();

            return BadRequest();
        }
    }
}