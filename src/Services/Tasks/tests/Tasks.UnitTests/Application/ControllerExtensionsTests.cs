using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Tasks.API.Application.Queries;
using Tasks.API.Controllers;
using Tasks.API.Extensions;
using Xunit;

namespace Tasks.UnitTests.Application
{
    public class ControllerExtensionsTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IRequest<bool>> _mockRequest;
        private readonly Mock<ILabelQueries> _mockLabelQueries;

        public ControllerExtensionsTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLabelQueries = new Mock<ILabelQueries>();
            _mockRequest = new Mock<IRequest<bool>>();
        }

        [Fact]
        public async void RequestExecutionResultAsync_WithFailedRequestResult_ReturnsBadRequest()
        {
            //Arrange
            _mockMediator.Setup(m => m.Send(_mockRequest.Object, default))
                .Returns(Task.FromResult(false));

            //Act
            //Since we are testing the Extension method for the Controller, we don't mockup the controller
            //We create a 'real' controller and provide it as an argument.
            var labelsController = new LabelsController(
                _mockMediator.Object, _mockLabelQueries.Object);

            var actionResult = await ControllerExtensions.RequestExecutionResultAsync(
                labelsController, _mockMediator.Object, _mockRequest.Object);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void RequestExecutionResultAsync_WithOkRequestResult_ReturnsOk()
        {
            //Arrange
            _mockMediator.Setup(m => m.Send(_mockRequest.Object, default))
                .Returns(Task.FromResult(true));

            //Act
            //Since we are testing the Extension method for the Controller, we don't mockup the controller
            //We create a 'real' controller and provide it as an argument.
            var labelsController = new LabelsController(
                _mockMediator.Object, _mockLabelQueries.Object);

            var actionResult = await ControllerExtensions.RequestExecutionResultAsync(
                labelsController, _mockMediator.Object, _mockRequest.Object);

            //Assert
            Assert.IsType<OkResult>(actionResult);
        }
    }
}
