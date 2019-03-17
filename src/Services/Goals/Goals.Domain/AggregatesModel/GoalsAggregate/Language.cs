using System.Collections.Generic;
using System.Linq;
using Goals.Domain.Exceptions;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public class Language 
        : Enumeration
    {
        public static Language Russian = new Language(1, nameof(Russian).ToLowerInvariant());
        public static Language English = new Language(2, nameof(English).ToLowerInvariant());

        protected Language() { }

        public Language(int id, string name) : base(id, name) { }

        public IEnumerable<Language> List() =>
            new[] { Russian, English };

        public Language FromName(string name)
        {
            var language = List().SingleOrDefault(l => l.Name == name);
            return language ?? throw new GoalsDomainException($"Possible values for GoalViewOption: {string.Join(", ", List().Select(o => o.Name))}");
        }

        public Language From(int id)
        {
            var language = List().SingleOrDefault(l => l.Id == id);
            return language ?? throw new GoalsDomainException($"Possible values for GoalViewOption: {string.Join(", ", List().Select(o => o.Id))}");
        }
    }
}
