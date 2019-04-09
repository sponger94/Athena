using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tasks.Domain.AggregatesModel.ProjectsAggregate;
using Tasks.Domain.SeedWork;
using UserTask = Tasks.Domain.AggregatesModel.UserTasksAggregate.UserTask;
using Microsoft.EntityFrameworkCore.Storage;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;

namespace Tasks.Infrastructure
{
    public class TasksContext : DbContext, IUnitOfWork
    {
        public const string DefaultSchema = "tasks";

        public DbSet<Project> Projects { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        private readonly IMediator _mediator;
        public IDbContextTransaction CurrentTransaction { get; private set; }

        private TasksContext(DbContextOptions<TasksContext> options) : base (options) { }

        public TasksContext(DbContextOptions<TasksContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            //TODO: Remove this
            System.Diagnostics.Debug.WriteLine("TasksContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}
