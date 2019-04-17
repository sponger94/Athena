using System;
using Tasks.API.Application.Queries;
using Xunit;


namespace Tasks.UnitTests
{
    public class UnitTest1
    {
        private UserTaskQueries sut;

        [Fact]
        public void Test1()
        {
            sut = new UserTaskQueries("Server=.;Initial Catalog=Athena.Services.Goals;Integrated Security=true");
            var result =  sut.GetTaskAsync(1).Result;

            var abc = 15;
        }
    }
}
