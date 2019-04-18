using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Tasks.API.Application.Queries;
using Tasks.API.Controllers;
using Tasks.API.Services;
using Xunit;

namespace Tasks.UnitTests.Application
{
    public class UserTasksControllerTests 
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IUserTaskQueries> _taskQueriesMock;

        public UserTasksControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _identityServiceMock = new Mock<IIdentityService>();
            _taskQueriesMock = new Mock<IUserTaskQueries>();
        }

        [Fact]
        public async void GetUserTaskByIdAsync_WithExistingId_ReturnsUserTask()
        {
            //Arrange
            var fakeId = 12;
            var fakeDynamicResult = new UserTask();

            _taskQueriesMock.Setup(t => t.GetTaskAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(fakeDynamicResult));

            //Act
            var userTasksController = new UserTasksController(
                _mediatorMock.Object, _identityServiceMock.Object, _taskQueriesMock.Object);
            var actionResult = await userTasksController.GetUserTaskByIdAsync(fakeId);
            _identityServiceMock.Setup(i => i.GetUserIdentity())
                .Returns(Guid.NewGuid().ToString());

        }
    }
}
