﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tasks.API.Application.Commands.Label;
using Tasks.API.Application.Queries;
using Tasks.API.Extensions;

namespace Tasks.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILabelQueries _labelQueries;

        public LabelsController(IMediator mediator,
            ILabelQueries labelQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _labelQueries = labelQueries ?? throw new ArgumentNullException(nameof(labelQueries));
        }

        //GET api/v1/[controller]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Label>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLabelsAsync()
        {
            var labels = await _labelQueries.GetLabelsAsync();
            return Ok(labels);
        }

        //GET api/v1/[controller]/labels/5
        [HttpGet]
        [Route("{labelId:int}")]
        [ProducesResponseType(typeof(IEnumerable<Label>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLabelByIdAsync([FromQuery] int labelId)
        {
            var labels = await _labelQueries.GetLabelByIdAsync(labelId);
            return Ok(labels);
        }

        //POST api/v1/[controller]/create
        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateLabelAsync([FromBody] CreateLabelCommand createLabelCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, createLabelCommand);
        }

        //DELETE api/v1/[controller]/delete
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveLabelAsync([FromBody] RemoveLabelCommand removeLabelCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, removeLabelCommand);
        }

        //PUT api/v1/[controller]/update
        [HttpPut]
        [Route("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateLabelAsync([FromBody] UpdateLabelCommand updateLabelCommand)
        {
            return await this.RequestExecutionResultAsync(_mediator, updateLabelCommand);
        }
    }
}
