using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Tasks.API.Application.Queries;
using Tasks.API.Services;
using Xunit;

namespace Tasks.UnitTests.Application
{
    public class ProjectControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IIdentityService> _identityServiceMock;

        public ProjectControllerTests()
        {

        }
    }
}
