using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.ProjectsAggregate;

namespace Tasks.API.Application.Commands.Project
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectCommandHandler(IMediator mediator, 
            IProjectRepository projectRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetAsync(request.ProjectId);
            if (project == null)
                return false;

            project.SetName(request.Name);
            project.SetColor(Color.FromArgb(request.Argb));
            _projectRepository.Update(project);

            return await _projectRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
