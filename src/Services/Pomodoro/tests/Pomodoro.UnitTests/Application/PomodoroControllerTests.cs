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

            _pomodoroRepositoryMock.Verify(p => p.GetItemsByIdsAsync(It.IsAny<string>()), Times.Never());
            Assert.Equal(expectedCurrentPage, model.PageIndex);
            Assert.Equal(expectedPageSize, model.PageSize);
            Assert.Equal(expectedTotalItems, model.Count);
        }
    }
}
