using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.API.Application.Queries;
using Tasks.API.Controllers;
using Tasks.API.Services;
using Xunit;

namespace Tasks.UnitTests.Application
{
    public class ProjectControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IUserTaskQueries> _taskQueriesMock;

        public ProjectControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _identityServiceMock = new Mock<IIdentityService>();
            _taskQueriesMock = new Mock<IUserTaskQueries>();
        }

        #region GetProjectsAsync

        [Fact]
        public async void GetProjectsAsync_ReturnsListOfProjects()
        {
            //Arrange
            var fakeDynamicResult = new List<ProjectSummary>().AsEnumerable();
            _identityServiceMock.Setup(i => i.GetUserIdentity())
                .Returns(Guid.NewGuid().ToString());

            _taskQueriesMock.Setup(t => t.GetProjectsAsync(It.IsAny<Guid>(), 20, 0))
                .Returns(Task.FromResult(fakeDynamicResult));

            //Act
            var projectsController = new ProjectsController(
              _mediatorMock.Object, _identityServiceMock.Object, _taskQueriesMock.Object);
            var actionResult = await projectsController.GetProjectsAsync();

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsAssignableFrom<IEnumerable<ProjectSummary>>(okObjectResult.Value);
        }

        #endregion
    }
}
