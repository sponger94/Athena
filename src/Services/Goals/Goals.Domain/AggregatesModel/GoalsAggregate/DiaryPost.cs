using System;
using System.Collections.Generic;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class DiaryPost
        : ValueObject
    {
        public int IdentityGuid { get; private set; }
        public int DiaryPostId { get; private set; }
        public DateTime PostedTime { get; private set; }
        public string Content { get; private set; }

        private List<Comment> _comments;
        public IReadOnlyCollection<Comment> Comments => _comments;

        private DiaryPost()
        {
            _comments = new List<Comment>();
        }

        public DiaryPost(int identityGuid, int diaryPostId, DateTime postedTime, string content)
            : this()
        {
            IdentityGuid = identityGuid;
            DiaryPostId = diaryPostId;
            PostedTime = postedTime;
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
