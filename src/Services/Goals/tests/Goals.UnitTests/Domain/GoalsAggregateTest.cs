using Goals.Domain.AggregatesModel.GoalsAggregate;
using System;
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
