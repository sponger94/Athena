using System.Collections.Generic;
using System.Linq;
using Goals.Domain.Exceptions;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class GoalStatus
        : Enumeration
    {
        public static GoalStatus InProgress = new GoalStatus(1, nameof(InProgress).ToLowerInvariant());
        public static GoalStatus Completed = new GoalStatus(2, nameof(Completed).ToLowerInvariant());
        public static GoalStatus Failed = new GoalStatus(3, nameof(Failed).ToLowerInvariant());

        public GoalStatus(int id, string name)
            : base(id, name)
        { }

        public static IEnumerable<GoalStatus> List() =>
            new[] { InProgress, Completed, Failed };

        public static GoalStatus FromName(string name)
        {
            var status = List().SingleOrDefault(s => s.Name == name);
            return status ?? throw new GoalsDomainException($"Possible values for GoalStatus: " +
                                                            $"{string.Join(", ", List().Select(s => s.Name))}");
        } 
        
        public static GoalStatus From(int id)
        {
            var status = List().SingleOrDefault(s => s.Id == id);
            return status ?? throw new GoalsDomainException($"Possible values for GoalStatus: " +
                                                            $"{string.Join(", ", List().Select(s => s.Id))}");
        }
    }
}
