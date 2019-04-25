using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.API.Application.Commands.UserTask
{
    public class RemoveUserTaskCommandHandler : IRequestHandler<RemoveUserTaskCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IUserTaskRepository _userTaskRepository;

        public RemoveUserTaskCommandHandler(IMediator mediator, IUserTaskRepository userTaskRepository)
        {
            _mediator = mediator;
            _userTaskRepository = userTaskRepository;
        }

        public async Task<bool> Handle(RemoveUserTaskCommand request, CancellationToken cancellationToken)
        {
            var userTask = await _userTaskRepository.GetAsync(request.UserTaskId);
            _userTaskRepository.Remove(userTask);

            return await _userTaskRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
