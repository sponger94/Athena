using Goals.Domain.AggregatesModel.GoalsAggregate;
using System;
using Goals.Domain.Exceptions;
using Xunit;

namespace Goals.UnitTests.Domain
{
    public class GoalsAggregateTest
    {
        public string IdentityGuid { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public DateTime? DateDue { get; private set; }

        public byte[] Image { get; private set; }

        public GoalSettings GoalSettings { get; private set; }

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
    }
}
