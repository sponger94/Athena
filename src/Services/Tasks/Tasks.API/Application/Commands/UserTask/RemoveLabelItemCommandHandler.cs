using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.LabelsAggregate;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.API.Application.Commands.UserTask
{
    public class RemoveLabelItemCommandHandler : IRequestHandler<RemoveLabelItemCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ILabelRepository _labelRepository;
        private readonly IUserTaskRepository _userTaskRepository;

        public RemoveLabelItemCommandHandler(IMediator mediator,
            ILabelRepository labelRepository,
            IUserTaskRepository userTaskRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
            _userTaskRepository = userTaskRepository ?? throw new ArgumentNullException(nameof(userTaskRepository));
        }

        public async Task<bool> Handle(RemoveLabelItemCommand request, CancellationToken cancellationToken)
        {
            var label = await _labelRepository.GetAsync(request.LabelId);
            if (label == null)
                return false;

            var userTask = await _userTaskRepository.GetAsync(request.UserTaskId);
            if (userTask == null)
                return false;

            userTask.RemoveLabel(label);
            _userTaskRepository.Update(userTask);

            return await _userTaskRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
