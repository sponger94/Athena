using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        #region GetUserTaskByIdAsync

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

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsAssignableFrom<UserTask>(okObjectResult.Value);
        }

        [Fact]
        public async void GetUserTaskByIdAsync_NonExistingId_ReturnsNotFound()
        {
            //Arrange
            var fakeNonExistingId = 32;
            _taskQueriesMock.Setup(t => t.GetTaskAsync(fakeNonExistingId))
                .Throws(new Exception());

            //Act
            var userTasksController = new UserTasksController(
               _mediatorMock.Object, _identityServiceMock.Object, _taskQueriesMock.Object);
            var actionResult = await userTasksController.GetUserTaskByIdAsync(fakeNonExistingId);

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        #endregion

        #region GetUserTasksAsync

        [Fact]
        public async void GetUserTasksAsync_ReturnsListOfUserTasks()
        {
            //Arrange
            var fakeDynamicResult = new List<UserTaskSummary>().AsEnumerable();
            _identityServiceMock.Setup(i => i.GetUserIdentity())
                .Returns(Guid.NewGuid().ToString());

            _taskQueriesMock.Setup(t => t.GetUserTasksAsync(It.IsAny<Guid>(), 20, 0))
                .Returns(Task.FromResult(fakeDynamicResult));

            //Act
            var userTasksController = new UserTasksController(
              _mediatorMock.Object, _identityServiceMock.Object, _taskQueriesMock.Object);
            var actionResult = await userTasksController.GetUserTasksAsync();

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsAssignableFrom<IEnumerable<UserTaskSummary>>(okObjectResult.Value);
        }

        #endregion

        #region GetProjectUserTasksAsync

        [Fact]
        public async void GetProjectUserTasksAsync_ReturnsListOfUserTasks()
        {
            //Arrange
            var fakeDynamicResult = new List<UserTaskSummary>().AsEnumerable();
            _identityServiceMock.Setup(i => i.GetUserIdentity())
                .Returns(Guid.NewGuid().ToString());

            _taskQueriesMock.Setup(t => t.GetProjectUserTasksAsync(It.IsAny<Guid>(), 5, 20, 0))
                .Returns(Task.FromResult(fakeDynamicResult));

            //Act
            var userTasksController = new UserTasksController(
              _mediatorMock.Object, _identityServiceMock.Object, _taskQueriesMock.Object);
            var actionResult = await userTasksController.GetUserTasksAsync();

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsAssignableFrom<IEnumerable<UserTaskSummary>>(okObjectResult.Value);
        }

        #endregion
    }
}
