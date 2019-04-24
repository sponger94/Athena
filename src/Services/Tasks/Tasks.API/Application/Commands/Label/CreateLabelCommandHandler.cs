using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.API.Services;
using Tasks.Domain.AggregatesModel.LabelsAggregate;
using Tasks.Domain.AggregatesModel.ProjectsAggregate;

namespace Tasks.API.Application.Commands.Label
{
    public class CreateLabelCommandHandler : IRequestHandler<CreateLabelCommand, bool>
    {   
        private readonly IMediator _mediator;
        private readonly ILabelRepository _labelRepository;

        public CreateLabelCommandHandler(IMediator mediator, 
            ILabelRepository labelRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
        }

        public async Task<bool> Handle(CreateLabelCommand request, CancellationToken cancellationToken)
        {
            //TODO: Publish integration event

            var label = new Domain.AggregatesModel.UserTasksAggregate.Label(request.Name, Color.FromArgb(request.Argb));
            _labelRepository.Add(label);

            return await _labelRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
