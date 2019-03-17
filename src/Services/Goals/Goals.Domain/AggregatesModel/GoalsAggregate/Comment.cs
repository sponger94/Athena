using System;
using System.Collections.Generic;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class Comment
        : ValueObject
    {
        public int IdentityGuid { get; private set; }
        public int DiaryPostId { get; private set; }
        public DateTime PostedTime { get; private set; }
        public string Content { get; private set; }

        private Comment()
        { }

        public Comment(int identityGuid, int diaryPostId, string content)
        {
            IdentityGuid = identityGuid;
            DiaryPostId = diaryPostId;
            PostedTime = DateTime.Now;
            Content = content;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return IdentityGuid;
            yield return DiaryPostId;
            yield return PostedTime;
            yield return Content;
        }
    }
}
