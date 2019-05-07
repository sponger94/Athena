using Athena.Pomodoro.API;
using Athena.Pomodoro.API.Infrastructure.Repositories;
using Athena.Pomodoro.API.IntegrationEvents;
using Athena.Pomodoro.API.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Pomodoro.UnitTests
{
    public class PomodoroControllerTests
    {
        private readonly Mock<IPomodoroIntegrationEventService> _pomodoroIntegrationMock;
        private readonly Mock<IPomodoroRepository> _pomodoroRepositoryMock;
        private readonly PomodoroSettings _settings;

        public PomodoroControllerTests()
        {
            _pomodoroIntegrationMock = new Mock<IPomodoroIntegrationEventService>();
            _pomodoroRepositoryMock = new Mock<IPomodoroRepository>();
            _settings = new PomodoroSettings();
        }

        private PaginatedItemsViewModel<Athena.Pomodoro.API.Model.Pomodoro> GetFakeItems(int pageIndex, int pageSize, int count)
        {
            //var fakeItems = new List<>
            //return new PaginatedItemsViewModel<Athena.Pomodoro.API.Model.Pomodoro>(
            //    pageIndex,
            //    pageSize,
            //    count,
            //    new { 
            //            new 
            //        });
        }
        [Fact]
        public void ItemsAsync_IdsStringNull_ReturnsPaginatedListOfAllItems()
        {
            //Arrange
            var fakePage = 2;

        }
    }
}
