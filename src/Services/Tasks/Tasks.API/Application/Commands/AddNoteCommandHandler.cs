using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.API.Application.Commands
{
    public class AddNoteCommandHandler : IRequestHandler<AddNoteCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IUserTaskRepository _userTaskRepository;

        public AddNoteCommandHandler(IMediator mediator, IUserTaskRepository userTaskRepository)
        {
            _mediator = mediator;
            _userTaskRepository = userTaskRepository;
        }

        public async Task<bool> Handle(AddNoteCommand request, CancellationToken cancellationToken)
        {
            var userTask = await _userTaskRepository.GetAsync(request.UserTaskId);
            if (userTask == null)
                return false;

            var note = new Note(request.Content);
            userTask.AddNote(note);

            return await _userTaskRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
