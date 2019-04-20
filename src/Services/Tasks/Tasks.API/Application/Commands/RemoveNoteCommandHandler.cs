using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.API.Application.Commands
{
    public class RemoveNoteCommandHandler : IRequestHandler<RemoveNoteCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IUserTaskRepository _userTaskRepository;

        public RemoveNoteCommandHandler(IMediator mediator, IUserTaskRepository userTaskRepository)
        {
            _mediator = mediator;
            _userTaskRepository = userTaskRepository;
        }

        public async Task<bool> Handle(RemoveNoteCommand request, CancellationToken cancellationToken)
        {
            var userTask = await _userTaskRepository.GetAsync(request.UserTaskId);
            if (userTask == null)
                return false;

            var note = new Note(request.Content);
            userTask.RemoveNote(note);

            return await _userTaskRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
