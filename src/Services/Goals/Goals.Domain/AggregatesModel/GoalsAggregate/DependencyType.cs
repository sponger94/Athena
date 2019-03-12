using System.Collections.Generic;
using System.Linq;
using Goals.Domain.Exceptions;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class DependencyType
        : Enumeration
    {
        public static DependencyType SubTask = new DependencyType(1, nameof(SubTask).ToLowerInvariant());
        public static DependencyType Impediment = new DependencyType(2, nameof(Impediment).ToLowerInvariant());

        protected DependencyType()
        { }

        public DependencyType(int id, string name) : base(id, name)
        { }

        public static IEnumerable<DependencyType> List() =>
            new[] { SubTask, Impediment };

        public static DependencyType FromName(string name)
        {
            var option = List().SingleOrDefault(o => o.Name == name);
            return option ?? throw new GoalsDomainException($"Possible values for DependencyType: {string.Join(", ", List().Select(o => o.Name))}");
        }

        public static DependencyType From(int id)
        {
            var option = List().SingleOrDefault(o => o.Id == id);
            return option ?? throw new GoalsDomainException($"Possible values for DependencyType: {string.Join(", ", List().Select(o => o.Id))}");
        }
    }
}
