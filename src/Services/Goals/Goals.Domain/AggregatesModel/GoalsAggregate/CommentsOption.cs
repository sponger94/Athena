using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class CommentsOption : Enumeration
    {
        public static CommentsOption Public = new CommentsOption(1, nameof(Public).ToLowerInvariant());
        public static CommentsOption FriendsOnly = new CommentsOption(2, nameof(FriendsOnly).ToLowerInvariant());
        public static CommentsOption SomeFriends = new CommentsOption(3, nameof(SomeFriends).ToLowerInvariant());
        public static CommentsOption OnlyMe = new CommentsOption(4, nameof(OnlyMe).ToLowerInvariant());

        protected CommentsOption() { }

        public CommentsOption(int id, string name) : base(id, name) { }

        public static IEnumerable<CommentsOption> List() =>
            new[] {Public, FriendsOnly, SomeFriends, OnlyMe};

        public static CommentsOption FromName(string name)
        {
            var option = List().SingleOrDefault(o => o.Name == name);
            return option ?? throw new Exception($"Possible values for GoalViewOption: {string.Join(", ", List().Select(o => o.Name))}");
        }

        public static CommentsOption From(int id)
        {
            var option = List().SingleOrDefault(o => o.Id == id);
            return option ?? throw new Exception($"Possible values for GoalViewOption: {string.Join(", ", List().Select(o => o.Id))}");
        }
    }
}
