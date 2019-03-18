using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.TasksAggregate
{
    public class SubTask : ValueObject
    {
        public string Name { get; private set; }

        public bool IsCompleted { get; private set; }

        private SubTask()
        { }

        public SubTask(string name, bool isCompleted)
        {
            Name = name;
            IsCompleted = isCompleted;
        }

        //public void SetTaskCompleted()
        //{
        //    IsCompleted = true;
        //}

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return IsCompleted;
        }
    }
}
