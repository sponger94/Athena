using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tasks.API.Application.Commands.Label;
using Tasks.API.Application.Queries;
using Tasks.API.Services;

namespace Tasks.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly ILabelQueries _labelQueries;

        public LabelsController(IMediator mediator,
            IIdentityService identityService,
            ILabelQueries labelQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
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

        //GET api/v1/[controller]
        [HttpGet]
        [Route("{labelId:int}")]
        [ProducesResponseType(typeof(IEnumerable<Label>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLabelByIdAsync([FromQuery] int labelId)
        {
            var labels = await _labelQueries.GetLabelByIdAsync(labelId);
            return Ok(labels);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateLabelAsync([FromBody] CreateLabelCommand createLabelCommand)
        {
            bool result = await _mediator.Send(createLabelCommand);

            if (result)
                return Ok();

            return BadRequest();
        }
    }
}
