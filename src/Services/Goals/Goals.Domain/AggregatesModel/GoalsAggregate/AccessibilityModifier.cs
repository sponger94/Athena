using System.Collections.Generic;
using System.Linq;
using Goals.Domain.Exceptions;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class AccessibilityModifier : Enumeration
    {
        public static AccessibilityModifier Public = new AccessibilityModifier(1, nameof(Public).ToLowerInvariant());
        public static AccessibilityModifier FriendsOnly = new AccessibilityModifier(2, nameof(FriendsOnly).ToLowerInvariant());
        public static AccessibilityModifier SomeFriends = new AccessibilityModifier(3, nameof(SomeFriends).ToLowerInvariant());
        public static AccessibilityModifier OnlyMe = new AccessibilityModifier(4, nameof(OnlyMe).ToLowerInvariant());

        protected AccessibilityModifier() { }

        public AccessibilityModifier(int id, string name) : base(id, name) { }

        public static IEnumerable<AccessibilityModifier> List() =>
            new[] { Public, FriendsOnly, SomeFriends, OnlyMe };

        public static AccessibilityModifier FromName(string name)
        {
            var option = List().SingleOrDefault(o => o.Name == name);
            return option ?? throw new GoalsDomainException($"Possible values for GoalViewOption: {string.Join(", ", List().Select(o => o.Name))}");
        }

        public static AccessibilityModifier From(int id)
        {
            var option = List().SingleOrDefault(o => o.Id == id);
            return option ?? throw new GoalsDomainException($"Possible values for GoalViewOption: {string.Join(", ", List().Select(o => o.Id))}");
        }
    }
}
