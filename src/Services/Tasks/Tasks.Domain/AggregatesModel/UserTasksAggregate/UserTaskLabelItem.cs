using System.Collections.Generic;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.UserTasksAggregate
{
    public class UserTaskLabelItem
        : ValueObject
    {
        public int LabelId { get; private set; }

        public Label Label { get; private set; }

        private UserTaskLabelItem()
        { }

        public UserTaskLabelItem(int labelId)
        {
            LabelId = labelId;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return LabelId;
        }
    }
}
