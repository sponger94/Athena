using MediatR;
using Moq;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Tasks.API.Application.Commands.Label;
using Tasks.API.Application.Commands.UserTask;
using Tasks.Domain.AggregatesModel.LabelsAggregate;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;
using Tasks.Domain.SeedWork;
using Xunit;

namespace Tasks.UnitTests.Application.Commands
{
    public class AddLabelItemCommandHandlerTests
    {
        private readonly AddLabelItemCommand _addLabelItemCommand;
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILabelRepository> _mockLabelRepository;
        private readonly Mock<IUserTaskRepository> _mockUserTaskRepository;

        public AddLabelItemCommandHandlerTests()
        {
            _addLabelItemCommand = new AddLabelItemCommand(1, 2, "test name");
            _mockMediator = new Mock<IMediator>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLabelRepository = new Mock<ILabelRepository>();
            _mockUserTaskRepository = new Mock<IUserTaskRepository>();
        }

        [Fact]
        public async void Handle_GetAsyncNullResult_ReturnsFalse()
        {
            //Arrange
            var fakeId = 15;
            UserTask fakeNullResult = null;
            _mockUserTaskRepository.Setup(u => u.GetAsync(fakeId))
                .Returns(Task.FromResult(fakeNullResult));

            //Act
            var commandHandler = new AddLabelItemCommandHandler(
                _mockMediator.Object,
                _mockLabelRepository.Object,
                _mockUserTaskRepository.Object);

            var executionResult = await commandHandler.Handle(_addLabelItemCommand, new CancellationToken());

            //Assert
            Assert.False(executionResult);
        }

        [Fact]
        public async void Handle_LabelIsNullAndSaveResultIsFalse_CreatesANewLabelAndReturnsFalse()
        {
            //Arrange
            var cancellationToken = new CancellationToken();
            UserTask fakeUserTask = new UserTask("test");
            _mockUserTaskRepository.Setup(u => u.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(fakeUserTask));

            _mockMediator.Setup(m => m.Send(It.IsAny<CreateLabelCommand>(), cancellationToken))
                .Returns(Task.FromResult(false));

            //Act
            var commandHandler = new AddLabelItemCommandHandler(
                _mockMediator.Object,
                _mockLabelRepository.Object,
                _mockUserTaskRepository.Object);

            var executionResult = await commandHandler.Handle(_addLabelItemCommand, cancellationToken);

            //Assert
            _mockMediator.Verify(m => m.Send(It.IsAny<CreateLabelCommand>(), cancellationToken), Times.Once());
            Assert.False(executionResult);
        }

        [Fact]
        public async void Handle_LabelIsNullAndSaveResultIsTrue_CreatesANewLabelAndReturnsTrue()
        {
            //Arrange
            var cancellationToken = new CancellationToken();
            UserTask fakeUserTask = new UserTask("test");
            _mockUserTaskRepository.Setup(u => u.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(fakeUserTask));

            _mockMediator.Setup(m => m.Send(It.IsAny<CreateLabelCommand>(), cancellationToken))
                .Returns(Task.FromResult(true));

            _mockUserTaskRepository.Setup(u => u.UnitOfWork)
                .Returns(_mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(u => u.SaveEntitiesAsync(cancellationToken))
                .Returns(Task.FromResult(true));

            //Act
            var commandHandler = new AddLabelItemCommandHandler(
                _mockMediator.Object,
                _mockLabelRepository.Object,
                _mockUserTaskRepository.Object);

            var executionResult = await commandHandler.Handle(_addLabelItemCommand, cancellationToken);

            //Assert
            _mockMediator.Verify(m => m.Send(It.IsAny<CreateLabelCommand>(), cancellationToken), Times.Once());
            _mockUserTaskRepository.Verify(u => u.Update(fakeUserTask), Times.Once());
            Assert.True(executionResult);
        }

        [Fact]
        public async void Handle_LabelFound_ReturnsTrue()
        {
            //Arrange
            var cancellationToken = new CancellationToken();
            UserTask fakeUserTask = new UserTask("test");
            _mockUserTaskRepository.Setup(u => u.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(fakeUserTask));

            var fakeLabel = new Label("test label", Color.AliceBlue);
            _mockLabelRepository.Setup(l => l.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(fakeLabel));

            _mockUserTaskRepository.Setup(u => u.UnitOfWork)
                .Returns(_mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(u => u.SaveEntitiesAsync(cancellationToken))
                .Returns(Task.FromResult(true));

            //Act
            var commandHandler = new AddLabelItemCommandHandler(
                _mockMediator.Object,
                _mockLabelRepository.Object,
                _mockUserTaskRepository.Object);

            var executionResult = await commandHandler.Handle(_addLabelItemCommand, cancellationToken);

            //Assert
            _mockMediator.Verify(m => m.Send(It.IsAny<CreateLabelCommand>(), cancellationToken), Times.Never());
            _mockUserTaskRepository.Verify(u => u.Update(fakeUserTask), Times.Once());
            Assert.True(executionResult);
        }
    }
}
