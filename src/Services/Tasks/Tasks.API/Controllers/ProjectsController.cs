﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasks.API.Application.Commands;
using Tasks.API.Application.Commands.Project;
using Tasks.API.Application.Queries;
using Tasks.API.Extensions;
using Tasks.API.Services;

namespace Tasks.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly IUserTaskQueries _taskQueries;

        public ProjectsController(IMediator mediator, 
            IIdentityService identityService, 
            IUserTaskQueries taskQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _taskQueries = taskQueries ?? throw new ArgumentNullException(nameof(taskQueries));
        }

        
        //GET api/v1/[controller][?pageSize=3&pageIndex=18]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProjectSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProjectsAsync([FromQuery] int pageSize = 20, [FromQuery] int pageIndex = 0)
        {
            var userId = _identityService.GetUserIdentity();
            var projectUserTasks = await _taskQueries.GetProjectsAsync(Guid.Parse(userId), pageSize, pageIndex);

            return Ok(projectUserTasks);
        }

        //POST api/v1/[controller]/create
        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProjectAsync([FromBody] CreateProjectCommand createProjectCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, createProjectCommand);
        }

        //DELETE api/v1/[controller]/delete
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProjectAsync([FromBody] RemoveProjectCommand removeProjectCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, removeProjectCommand);
        }

        //PUT api/v1/[controller]/update
        [HttpPut]
        [Route("update")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProjectAsync([FromBody] UpdateProjectCommand updateProjectCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, updateProjectCommand);
        }
    }
}