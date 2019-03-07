using System;
using System.Threading.Tasks;
using Goals.Domain.AggregatesModel.GoalsAggregate;
using Goals.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Goals.Infrastructure.Repositories
{
    public class GoalsRepository
        : IGoalRepository
    {
        private readonly GoalsContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public GoalsRepository(GoalsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Goal Add(Goal goal)
        {
            return _context.Goals.Add(goal).Entity;
        }

        public async Task<Goal> GetAsync(int goalId)
        {
            var goal = await _context.Goals.FindAsync(goalId);
            if (goal != null)
            {
                await _context.Entry(goal)
                    .Collection(g => g.SubGoals).LoadAsync();
                //TODO: Do I have to load owned entity?!
            }

            return goal;
        }

        public void Update(Goal goal)
        {
            _context.Entry(goal).State = EntityState.Modified;
        }

        public void Delete(int goalId)
        {
            //TODO: Delete an entity
        }
    }
}
