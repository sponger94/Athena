using MediatR;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Tasks.API.Application.Commands.Label;
using Tasks.Domain.AggregatesModel.LabelsAggregate;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.API.Application.Commands.UserTask
{
    public class AddLabelItemCommandHandler : IRequestHandler<AddLabelItemCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ILabelRepository _labelRepository;
        private readonly IUserTaskRepository _userTaskRepository;

        public AddLabelItemCommandHandler(IMediator mediator,
            ILabelRepository labelRepository,
            IUserTaskRepository userTaskRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
            _userTaskRepository = userTaskRepository ?? throw new ArgumentNullException(nameof(userTaskRepository));
        }

        public async Task<bool> Handle(AddLabelItemCommand request, CancellationToken cancellationToken)
        {
            var userTask = await _userTaskRepository.GetAsync(request.UserTaskId);
            if (userTask == null)
                return false;

            Domain.AggregatesModel.UserTasksAggregate.Label label;

            label = await _labelRepository.GetAsync(request.LabelId);
            if (label == null)
            {
                label = new Domain.AggregatesModel.UserTasksAggregate.Label(request.Name,
                    Color.FromArgb(request.Argb));
                var createLabelCommand = new CreateLabelCommand(request.Argb, request.Name);
                var result = await _mediator.Send(createLabelCommand, cancellationToken);

                if (!result)
                    return false;
            }

            userTask.AddLabel(label);
            _userTaskRepository.Update(userTask);

            return await _userTaskRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
