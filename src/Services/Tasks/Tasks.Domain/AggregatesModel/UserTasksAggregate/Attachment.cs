using System;
using System.Collections.Generic;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.UserTasksAggregate
{
    public class Attachment : ValueObject
    {
        public string Uri { get; private set; }

        private Attachment()
        { }

        public Attachment(string uri)
        {
            Uri = !string.IsNullOrWhiteSpace(uri)
                ? uri
                : throw new ArgumentNullException(nameof(uri));
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Uri;
        }
    }
}
