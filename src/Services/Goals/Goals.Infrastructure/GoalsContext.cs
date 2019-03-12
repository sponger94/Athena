using Goals.Domain.AggregatesModel.GoalsAggregate;
using Goals.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Goals.Infrastructure.EntitiesConfiguration;

namespace Goals.Infrastructure
{
    public class GoalsContext : DbContext, IUnitOfWork
    {
        public const string DefaultSchema = "goal";

        public DbSet<Goal> Goals { get; set; }

        private readonly IMediator _mediator;
        public IDbContextTransaction CurrentDbContextTransaction { get; private set; }

        private GoalsContext(DbContextOptions<GoalsContext> options)
            : base(options)
        { }

        public GoalsContext(DbContextOptions<GoalsContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new AccessibilityModifierEntityTypeConfiguration())
                .ApplyConfiguration(new DependencyTypeEntityTypeConfiguration())
                .ApplyConfiguration(new GoalDependencyEntityTypeConfiguration())
                .ApplyConfiguration(new GoalsEntityTypeConfiguration())
                .ApplyConfiguration(new GoalSettingsEntityTypeConfiguration())
                .ApplyConfiguration(new GoalStatusEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //Dispatch events collection before commiting data.
            await _mediator.DispatchDomainEventsAsync(this);

            await base.SaveChangesAsync();

            return true;
        }

        public async Task BeginTransactionAsync()
        {
            CurrentDbContextTransaction = CurrentDbContextTransaction ??
                                          (CurrentDbContextTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted));
        }

        public void RollbackTransaction()
        {
            try
            {
                CurrentDbContextTransaction.Rollback();
            }
            finally
            {
                if (CurrentDbContextTransaction != null)
                {
                    CurrentDbContextTransaction.Dispose();
                    CurrentDbContextTransaction = null;
                }
            }
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                CurrentDbContextTransaction.Commit();
            }
            catch (Exception e)
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (CurrentDbContextTransaction != null)
                {
                    CurrentDbContextTransaction.Dispose();
                    CurrentDbContextTransaction = null;
                }
            }
        }
    }

    public class GoalsContextDesignFactory : IDesignTimeDbContextFactory<GoalsContext>
    {
        private class NoMediator : IMediator
        {
            public Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }

            public Task Publish<TResponse>(TResponse notification, CancellationToken cancellationToken = default(CancellationToken)) where TResponse : INotification
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }
        }

        public GoalsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GoalsContext>()
                .UseSqlServer("Server=.;Initial Catalog=Athena.Services.Goals;Integrated Security=true");

            return new GoalsContext(optionsBuilder.Options, new NoMediator());
        }
    }
}
