using System;
using System.Linq;
using Goals.Domain.AggregatesModel.GoalsAggregate;
using Xunit;

namespace Tasks.UnitTests.Domain
{
    public class GoalsAggregateTest
    {
        private readonly Goal _testGoal;

        public GoalsAggregateTest()
        {
            var identityGuid = Guid.NewGuid().ToString();
            var title = "Test Goal";
            var description = "Test description";
            var goalSettings = new GoalSettings(AccessibilityModifier.Public.Id, AccessibilityModifier.Public.Id);
            _testGoal = new Goal(identityGuid, title, goalSettings, description);
        }

        #region ConstructorTests

        [Fact]
        public void CreatingGoal_WithEmptyIdentityGuidParam_ArgumentExceptionThrown()
        {
            //Arrange
            var identityGuid = string.Empty;
            var title = "Test Goal";
            var goalSettings = new GoalSettings(AccessibilityModifier.Public.Id, AccessibilityModifier.Public.Id);

            //Act - Assert
            Assert.Throws<ArgumentException>(() => new Goal(identityGuid, title, goalSettings));
        }

        [Fact]
        public void CreatingGoal_WithEmptyTitle_ArgumentExceptionThrown()
        {
            //Arrange
            var identityGuid = "testIdentity";
            var title = string.Empty;
            var goalSettings = new GoalSettings(AccessibilityModifier.Public.Id, AccessibilityModifier.Public.Id);

            //Act - Assert
            Assert.Throws<ArgumentException>(() => new Goal(identityGuid, title, goalSettings));
        }

        [Fact]
        public void CreatingGoal_WithNullGoalSettings_ArgumentNullExceptionThrown()
        {
            //Arrange
            var identityGuid = "testIdentity";
            var title = "Test Goal";

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => new Goal(identityGuid, title, null));
        }

        [Fact]
        public void CreatingGoal_WithDateTimeNow_ArgumentExceptionThrown()
        {
            //Arrange
            string identityGuid = Guid.NewGuid().ToString();
            string title = "Test Goal";
            string description = "Test description";
            var goalSettings = new GoalSettings(AccessibilityModifier.Public.Id, AccessibilityModifier.Public.Id);
            var dateDue = DateTime.Now;

            //Act - Assert
            Assert.Throws<ArgumentException>(() => new Goal(identityGuid, title, goalSettings, description, dateDue));
        }

        [Fact]
        public void CreatingGoal_WithValidParams_CreatedDomainEventAdded()
        {
            //Arrange
            string identityGuid = Guid.NewGuid().ToString();
            string title = "Test Goal";
            string description = "Test description";
            var goalSettings = new GoalSettings(AccessibilityModifier.Public.Id, AccessibilityModifier.Public.Id);

            //Act
            var goal = new Goal(identityGuid, title, goalSettings, description, null, null);

            //Assert
            Assert.Equal(1, goal.DomainEvents.Count);
        }

        #endregion
    }
}
