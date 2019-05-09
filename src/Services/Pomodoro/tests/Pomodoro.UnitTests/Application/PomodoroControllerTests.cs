using Athena.Pomodoros.API;
using Athena.Pomodoros.API.Infrastructure.Repositories;
using Athena.Pomodoros.API.IntegrationEvents;
using Athena.Pomodoros.API.ViewModel;
using Athena.Pomodoros.API.Model;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Athena.Pomodoros.API.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Pomodoros.UnitTests
{
    public class PomodoroControllerTests
    {
        private readonly Mock<IPomodoroIntegrationEventService> _pomodoroIntegrationMock;
        private readonly Mock<IPomodoroRepository> _pomodoroRepositoryMock;
        private readonly Mock<IOptionsSnapshot<PomodoroSettings>> _settingsMock;

        public PomodoroControllerTests()
        {
            _pomodoroIntegrationMock = new Mock<IPomodoroIntegrationEventService>();
            _pomodoroRepositoryMock = new Mock<IPomodoroRepository>();
            _settingsMock = new Mock<IOptionsSnapshot<PomodoroSettings>>();
        }

        private PaginatedItemsViewModel<Pomodoro> GetFakeItems(int pageIndex, int pageSize, int count)
        {
            var fakeItems = new List<Pomodoro>
            {
                new Pomodoro
                {
                    Id = 1,
                    ProjectId = 15,
                    ProjectName = "Test 1",
                    Time = DateTime.Now.Subtract(TimeSpan.FromDays(2)),
                    Duration = TimeSpan.FromMinutes(25),
                    UserId = "TestUserId_123456789"
                },
                new Pomodoro
                {
                    Id = 2,
                    ProjectId = 15,
                    ProjectName = "Test 2",
                    Time = DateTime.Now.Subtract(TimeSpan.FromDays(3)),
                    Duration = TimeSpan.FromMinutes(25),
                    UserId = "TestUserId_123456789"
                },
                new Pomodoro
                {
                    Id = 3,
                    ProjectId = 15,
                    ProjectName = "Test 3",
                    Time = DateTime.Now.Subtract(TimeSpan.FromDays(4)),
                    Duration = TimeSpan.FromMinutes(25),
                    UserId = "TestUserId_123456789"
                }
            };
            return new PaginatedItemsViewModel<Pomodoro>(
                pageIndex,
                pageSize,
                count,

                fakeItems);
        }


        #region ItemsAsyncTests

        [Fact]
        public async void ItemsAsync_IdsStringNull_ReturnsPaginatedListOfAllItems()
        {
            //Arrange
            var expectedCurrentPage = 2;
            var expectedPageSize = 10;
            var expectedTotalItems = 50;

            var fakePage = 2;
            var fakePageSize = 10;
            var fakeItems = GetFakeItems(fakePage, fakePageSize, expectedTotalItems);

            _pomodoroRepositoryMock.Setup(p => p.GetPomodoroItemsAsync
            (
                It.Is<int>(x => x == fakePage),
                It.Is<int>(x => x == fakePageSize)
            )).Returns(Task.FromResult(fakeItems));

            //Act
            var pomodoroController = new PomodoroController(
                _pomodoroRepositoryMock.Object,
                _settingsMock.Object,
                _pomodoroIntegrationMock.Object);
            var actionResult = await pomodoroController.ItemsAsync(fakePageSize, fakePage);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            var model = Assert.IsAssignableFrom<PaginatedItemsViewModel<Pomodoro>>(objectResult.Value);

            _pomodoroRepositoryMock.Verify(p => p.GetItemsByIdsAsync(It.IsAny<string>()), Times.Never);
            Assert.Equal(expectedCurrentPage, model.PageIndex);
            Assert.Equal(expectedPageSize, model.PageSize);
            Assert.Equal(expectedTotalItems, model.Count);
        }

        [Fact]
        public async void ItemsAsync_IdsStringIsNotNullAndGetItemsEmpty_ReturnsBadRequest()
        {
            //Arrange
            var fakePage = 2;
            var fakePageSize = 10;
            var fakeIds = "1,2,3,5,18";

            IEnumerable<Pomodoro> fakeResult = new List<Pomodoro>();
            _pomodoroRepositoryMock.Setup(p => p.GetItemsByIdsAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeResult));

            //Act
            var pomodoroController = new PomodoroController(_pomodoroRepositoryMock.Object,
                _settingsMock.Object,
                _pomodoroIntegrationMock.Object);
            var actionResult = await pomodoroController.ItemsAsync(fakePageSize, fakePage, fakeIds);

            //Assert
            _pomodoroRepositoryMock.Verify(p => p.GetPomodoroItemsAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void ItemsAsync_IdsStringIsNotNullAndGetItemsHasItems_ReturnsRequestedItems()
        {
            //Arrange
            var fakePage = 2;
            var fakePageSize = 10;
            var fakeTotalItems = 50;
            var fakeIds = "1,2,3,5,18";
            var fakeItems = GetFakeItems(fakePage, fakePageSize, fakeTotalItems);

            _pomodoroRepositoryMock.Setup(p => p.GetItemsByIdsAsync
            (
                It.Is<string>(x => x == fakeIds)
            )).Returns(Task.FromResult(fakeItems.Data));

            //Act
            var pomodoroController = new PomodoroController(
                _pomodoroRepositoryMock.Object,
                _settingsMock.Object,
                _pomodoroIntegrationMock.Object);
            var actionResult = await pomodoroController.ItemsAsync(fakePageSize, fakePage, fakeIds);

            //Assert
            _pomodoroRepositoryMock.Verify(p => p.GetPomodoroItemsAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsAssignableFrom<IEnumerable<Pomodoro>>(objectResult.Value);
        }

        #endregion

        #region ItemByIdAsyncTests

        [Fact]
        public async void ItemByIdAsync_IdEqualsZero_ReturnsBadRequest()
        {
            //Arrange
            var fakeId = 0;

            //Act
            var pomodoroController = new PomodoroController(_pomodoroRepositoryMock.Object,
                _settingsMock.Object,
                _pomodoroIntegrationMock.Object);
            var actionResult = await pomodoroController.ItemByIdAsync(fakeId);

            //Assert
            _pomodoroRepositoryMock.Verify(p => p.GetItemByIdAsync(fakeId), Times.Never);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void ItemByIdAsync_IdIsGreaterThanZeroAndItemIsNull_ReturnsNotFound()
        {
            //Arrange
            var fakeId = 15;
            _pomodoroRepositoryMock.Setup(p => p.GetItemByIdAsync(It.Is<int>(i => i == fakeId)))
                .Returns(Task.FromResult<Pomodoro>(null));

            //Act
            var pomodoroController = new PomodoroController(_pomodoroRepositoryMock.Object,
               _settingsMock.Object,
               _pomodoroIntegrationMock.Object);
            var actionResult = await pomodoroController.ItemByIdAsync(fakeId);

            //Assert
            _pomodoroRepositoryMock.Verify(p => p.GetItemByIdAsync(fakeId), Times.Once);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void ItemByIdAsync_IdIsGreaterThanZeroAndItemIsNotNull_ReturnsPomodoroItem()
        {
            //Arrange
            var fakeId = 15;
            var fakePomodoroItem = new Pomodoro();
            _pomodoroRepositoryMock.Setup(p => p.GetItemByIdAsync(It.Is<int>(i => i == fakeId)))
                .Returns(Task.FromResult(fakePomodoroItem));

            //Act
            var pomodoroController = new PomodoroController(_pomodoroRepositoryMock.Object,
               _settingsMock.Object,
               _pomodoroIntegrationMock.Object);
            var actionResult = await pomodoroController.ItemByIdAsync(fakeId);

            //Assert
            _pomodoroRepositoryMock.Verify(p => p.GetItemByIdAsync(fakeId), Times.Once);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsAssignableFrom<Pomodoro>(objectResult.Value);
        }

        #endregion


    }
}
