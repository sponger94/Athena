using System;
using System.Collections.Generic;
using Goals.Domain.Exceptions;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class Like
        : ValueObject
    {
        public string IdentityGuid { get; private set; }
        public int? GoalId { get; private set; }
        public int? CommentId { get; private set; }

        private Like()
        { }

        public Like(string identityGuid, int? goalId, int? commentId)
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identityGuid) ? identityGuid : throw new GoalsDomainException();
            if(goalId == null && commentId == null)
                throw new ArgumentNullException($"{nameof(goalId)} {nameof(commentId)}");

            GoalId = goalId;
            CommentId = commentId;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return IdentityGuid;
            yield return GoalId;
            yield return CommentId;
        }
    }
}
