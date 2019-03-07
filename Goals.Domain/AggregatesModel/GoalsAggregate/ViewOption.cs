using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class ViewOption : Enumeration
    {
        public static ViewOption Public = new ViewOption(1, nameof(Public).ToLowerInvariant());
        public static ViewOption FriendsOnly = new ViewOption(2, nameof(FriendsOnly).ToLowerInvariant());
        public static ViewOption SomeFriends = new ViewOption(3, nameof(SomeFriends).ToLowerInvariant());
        public static ViewOption OnlyMe = new ViewOption(4, nameof(OnlyMe).ToLowerInvariant());

        protected ViewOption() { }

        public ViewOption(int id, string name) : base(id, name) { }

        public static IEnumerable<ViewOption> List() =>
            new[] {Public, FriendsOnly, SomeFriends, OnlyMe};

        public static ViewOption FromName(string name)
        {
            var option = List().SingleOrDefault(o => o.Name == name);
            return option ?? throw new Exception($"Possible values for GoalViewOption: {string.Join(", ", List().Select(o => o.Name))}");
        }

        public static ViewOption From(int id)
        {
            var option = List().SingleOrDefault(o => o.Id == id);
            return option ?? throw new Exception($"Possible values for GoalViewOption: {string.Join(", ", List().Select(o => o.Id))}");
        }
    }
}
