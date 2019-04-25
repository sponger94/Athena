using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.ProjectsAggregate;

namespace Tasks.API.Application.Commands.Project
{
    public class RemoveProjectCommandHandler : IRequestHandler<RemoveProjectCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IProjectRepository _projectRepository;

        public RemoveProjectCommandHandler(IMediator mediator, 
            IProjectRepository projectRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<bool> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(request.ProjectId);
            _projectRepository.Remove(project);

            return await _projectRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
