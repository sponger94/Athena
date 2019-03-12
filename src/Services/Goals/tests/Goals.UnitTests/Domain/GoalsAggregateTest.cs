using Goals.Domain.AggregatesModel.GoalsAggregate;
using System;
using System.Linq;
using Goals.Domain.Events;
using Goals.Domain.Exceptions;
using Xunit;

namespace Goals.UnitTests.Domain
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

        [Fact]
        public void AddDependency_NullDependencyParam_ArgumentNullExceptionThrown()
        {
            //Act - Assert
            Assert.Throws<ArgumentNullException>(() =>_testGoal.AddDependency(null));
        }

        [Fact]
        public void AddDependency_DependencyDueDateLargerThanMainGoal_ArgumentNullExceptionThrown()
        {
            //Arrange
            var identityGuid = Guid.NewGuid().ToString();
            var title = "Test Goal";
            var goalSettings = new GoalSettings(AccessibilityModifier.Public.Id, AccessibilityModifier.Public.Id);
            _testGoal.SetDateDue(DateTime.Now.AddDays(1));
            
            //Setting SubGoals DueDate to be larger
            var subGoal = new Goal(identityGuid, title, goalSettings, dateDue: DateTime.Now.AddDays(3));
            var dependency = new GoalDependency(subGoal, DependencyType.SubTask.Id);
            
            //Act
            _testGoal.AddDependency(dependency);

            //Assert
            Assert.NotNull(_testGoal.DomainEvents.SingleOrDefault(d => d is DependencyDateDueExceededDomainEvent));
        }
    }
}
