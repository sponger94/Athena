﻿using System.Threading.Tasks;
using Goals.Domain.SeedWork;

namespace Goals.Domain.AggregatesModel.GoalsAggregate
{
    public interface IGoalRepository : IRepository<Goal>
    {
        Goal Add(Goal goal);

        void Update(Goal goal);

        void Delete(int goalId);

        Task<Goal> GetAsync(int goalId);
    }
}