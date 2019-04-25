using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.LabelsAggregate;

namespace Tasks.API.Application.Commands.Label
{
    public class RemoveLabelCommandHandler : IRequestHandler<RemoveLabelCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ILabelRepository _labelRepository;

        public RemoveLabelCommandHandler(IMediator mediator, 
            ILabelRepository labelRepository)
        {
            _mediator = mediator;
            _labelRepository = labelRepository;
        }

        public async Task<bool> Handle(RemoveLabelCommand request, CancellationToken cancellationToken)
        {
            var result = await _labelRepository.GetAsync(request.LabelId);
            _labelRepository.Remove(result);

            return await _labelRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
