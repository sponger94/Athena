using System.Collections.Generic;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.TasksAggregate
{
    public class Note : ValueObject
    {
        public string Content { get; private set; }

        private Note()
        { }

        public Note(string content)
        {
            Content = content;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Content;
        }
    }
}
