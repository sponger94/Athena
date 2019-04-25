using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.API.Application.Commands.UserTask
{
    public class RemoveAttachmentCommandHandler : IRequestHandler<RemoveAttachmentCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IUserTaskRepository _userTaskRepository;

        public RemoveAttachmentCommandHandler(IMediator mediator, IUserTaskRepository userTaskRepository)
        {
            _mediator = mediator;
            _userTaskRepository = userTaskRepository;
        }

        public async Task<bool> Handle(RemoveAttachmentCommand request, CancellationToken cancellationToken)
        {
            var userTask = await _userTaskRepository.GetAsync(request.UserTaskId);
            if (userTask == null)
                return false;

            var attachment = new Attachment(request.Uri);
            userTask.RemoveAttachment(attachment);

            return await _userTaskRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
