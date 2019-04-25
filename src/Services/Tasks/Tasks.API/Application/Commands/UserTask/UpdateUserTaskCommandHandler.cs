using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.API.Application.Commands.UserTask
{
    public class UpdateUserTaskCommandHandler : IRequestHandler<UpdateUserTaskCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IUserTaskRepository _userTaskRepository;

        public UpdateUserTaskCommandHandler(IMediator mediator, 
            IUserTaskRepository userTaskRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userTaskRepository = userTaskRepository ?? throw new ArgumentNullException(nameof(userTaskRepository));
        }

        public async Task<bool> Handle(UpdateUserTaskCommand request, CancellationToken cancellationToken)
        {
            var userTask = await _userTaskRepository.GetAsync(request.UserTaskId);
            if (userTask == null)
                return false;

            userTask.SetName(request.Name);
            userTask.SetIsCompleted(request.IsCompleted);
            userTask.SetProjectId(request.ProjectId);

            _userTaskRepository.Update(userTask);
            return await _userTaskRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
