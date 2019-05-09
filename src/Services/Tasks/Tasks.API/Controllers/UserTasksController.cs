using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tasks.API.Application.Commands.UserTask;
using Tasks.API.Application.Queries;
using Tasks.API.Extensions;
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

        //GET api/v1/[controller]/5
        [HttpGet]
        [Route("{userTaskId:int}")]
        [ProducesResponseType(typeof(UserTask), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
        public async Task<IActionResult> GetUserTasksAsync([FromQuery]int pageSize = 20, [FromQuery]int pageIndex = 0)
        {
            var userId = _identityService.GetUserIdentity();
            var userTasks = await _taskQueries.GetUserTasksAsync(Guid.Parse(userId), pageSize, pageIndex);

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
            return await this.RequestExecutionResultAsync(_mediator, createUserTaskCommand);
        }

        //DELETE api/v1/[controller]/delete
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUserTaskAsync([FromBody] RemoveUserTaskCommand removeUserTaskCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, removeUserTaskCommand);
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("add/attachment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddAttachmentAsync([FromBody] AddAttachmentCommand addAttachmentCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, addAttachmentCommand);
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("remove/attachment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveAttachmentAsync([FromBody] RemoveAttachmentCommand removeAttachmentCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, removeAttachmentCommand);
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("add/label")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddLabelItemAsync([FromBody] AddLabelItemCommand addLabelCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, addLabelCommand);
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("remove/label")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveLabelItemAsync([FromBody] RemoveLabelItemCommand removeLabelCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, removeLabelCommand);
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("add/note")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddNoteAsync([FromBody] AddNoteCommand addNoteCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, addNoteCommand);
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("remove/note")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveNoteAsync([FromBody] RemoveNoteCommand removeNoteCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, removeNoteCommand);
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("add/subtask")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddSubTaskAsync([FromBody] AddSubTaskCommand addSubTaskCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, addSubTaskCommand);
        }

        //PUT api/v1/[controller]
        [HttpPut]
        [Route("remove/subtask")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveSubTaskAsync([FromBody] RemoveSubTaskCommand removeSubTaskCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, removeSubTaskCommand);
        }
    }
}