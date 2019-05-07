using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Domain.AggregatesModel.ProjectsAggregate;
using Xunit;

namespace Tasks.UnitTests.Domain
{
    public class ProjectAggregateTests
    {
        public ProjectAggregateTests()
        { }

        #region Constructor Tests

        [Fact]
        public void CreateProject_Success()
        {
            //Arrange
            var identity = Guid.NewGuid().ToString();
            var name = "test project name";

            //Act
            var fakeProject = new Project(identity, name);

            //Assert
            Assert.NotNull(fakeProject);
        }

        [Fact]
        public void CreateProject_EmptyIdentity_ThrowsArgumentNullException()
        {
            //Arrange
            var identity = string.Empty;
            var name = "test project name";

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => new Project(identity, name));
        }

        [Fact]
        public void CreateProject_EmptyName_ThrowsArgumentNullException()
        {
            //Arrange
            var identity = Guid.NewGuid().ToString();
            var name = string.Empty;

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => new Project(identity, name));
        }

        #endregion


    }
}
