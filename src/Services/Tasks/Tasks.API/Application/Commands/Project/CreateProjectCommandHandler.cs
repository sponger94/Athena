using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.API.Services;
using Tasks.Domain.AggregatesModel.ProjectsAggregate;

namespace Tasks.API.Application.Commands.Project
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IMediator mediator,
            IIdentityService identityService,
            IProjectRepository projectRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            //TODO: Publish integration event

            var identityGuid = _identityService.GetUserIdentity();
            var project = new Domain.AggregatesModel.ProjectsAggregate.Project(identityGuid, request.Name, Color.FromArgb(request.Argb));
            _projectRepository.Add(project);

            return await _projectRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
