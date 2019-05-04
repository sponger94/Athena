using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Tasks.API.Application.Commands.UserTask;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;
using Tasks.Domain.SeedWork;
using Xunit;

namespace Tasks.UnitTests.Application.Commands
{
    public class AddAttachmentCommandHandlerTests
    {
        private readonly AddAttachmentCommand _addAttachmentCommand;
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserTaskRepository> _mockUserTaskRepository;

        public AddAttachmentCommandHandlerTests()
        {
            var fakeUserTaskId = 5;
            _addAttachmentCommand = new AddAttachmentCommand("uri", fakeUserTaskId);
            _mockMediator = new Mock<IMediator>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserTaskRepository = new Mock<IUserTaskRepository>();
        }

        [Fact]
        public async void Handle_GetAsyncReturnNull_ReturnsFalse()
        {
            //Arrange
            var fakeId = 15;
            UserTask fakeNullResult = null;
            _mockUserTaskRepository.Setup(u => u.GetAsync(fakeId))
                .Returns(Task.FromResult(fakeNullResult));

            //Act
            var commandHandler = new AddAttachmentCommandHandler(
                _mockMediator.Object,
                _mockUserTaskRepository.Object);

            var executionResult = await commandHandler.Handle(_addAttachmentCommand, new CancellationToken());

            //Assert
            Assert.False(executionResult);
        }

        [Fact]
        public async void Handle_ReturnsTrue()
        {
            //Arrange
            var fakeCancellationToken = new CancellationToken();
            UserTask fakeUserTask = new UserTask("test");
            _mockUserTaskRepository.Setup(u => u.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(fakeUserTask));

            _mockUserTaskRepository.Setup(u => u.UnitOfWork)
                .Returns(_mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(u => u.SaveEntitiesAsync(fakeCancellationToken))
                .Returns(Task.FromResult(true));

            //Act
            var commandHandler = new AddAttachmentCommandHandler(
                _mockMediator.Object,
                _mockUserTaskRepository.Object);

            var executionResult = await commandHandler.Handle(_addAttachmentCommand, fakeCancellationToken);

            //Assert
            Assert.True(executionResult);
        }
    }
}
