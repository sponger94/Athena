using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.LabelsAggregate;

namespace Tasks.API.Application.Commands.Label
{
    public class UpdateLabelCommandHandler : IRequestHandler<UpdateLabelCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ILabelRepository _labelRepository;

        public UpdateLabelCommandHandler(IMediator mediator, ILabelRepository labelRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
        }

        public async Task<bool> Handle(UpdateLabelCommand request, CancellationToken cancellationToken)
        {
            var label = await _labelRepository.GetAsync(request.LabelId);
            if (label == null)
                return false;

            label.SetColor(Color.FromArgb(request.Argb));
            label.SetName(request.Name);

            _labelRepository.Update(label);
            return await _labelRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
