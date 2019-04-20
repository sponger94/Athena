using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tasks.API.Services;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.API.Application.Commands
{
    public class CreateUserTaskCommandHandler
        : IRequestHandler<CreateUserTaskCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly IUserTaskRepository _userTaskRepository;

        public CreateUserTaskCommandHandler(IMediator mediator,
            IIdentityService identityService,
            IUserTaskRepository userTaskRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _userTaskRepository = userTaskRepository ?? throw new ArgumentNullException(nameof(userTaskRepository));
        }

        public async Task<bool> Handle(CreateUserTaskCommand message, CancellationToken cancellationToken)
        {
            //TODO: Publish integration event

            var userTask = new UserTask(message.Name, message.ProjectId);
            _userTaskRepository.Add(userTask);

            return await _userTaskRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
