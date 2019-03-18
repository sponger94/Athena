using System.Collections.Generic;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.TasksAggregate
{
    public class TaskLabelItem
        : ValueObject
    {
        public int LabelId { get; private set; }

        public Label Label { get; private set; }

        private TaskLabelItem()
        {
        }

        public TaskLabelItem(int labelId)
        {
            LabelId = labelId;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return LabelId;
        }
    }
}
