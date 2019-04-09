using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.AggregatesModel.UserTasksAggregate;
using Tasks.Domain.SeedWork;

namespace Tasks.Infrastructure.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly TasksContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public UserTaskRepository(TasksContext context)
        {
            _context = context;
        }

        public UserTask Add(UserTask userTask)
        {
            return _context.UserTasks.Add(userTask).Entity;
        }

        public async Task<UserTask> GetAsync(int userTaskId)
        {
            var userTask = await _context.UserTasks.FindAsync(userTaskId);
            if (userTask != null)
            {
                
                var attachmentsTask = _context.Entry(userTask)
                    .Collection(u => u.Attachments).LoadAsync();

                var labelItemsTask = _context.Entry(userTask)
                    .Collection(u => u.LabelItems).LoadAsync();

                var notesTask = _context.Entry(userTask)
                    .Collection(u => u.Notes).LoadAsync();

                var subTasks = _context.Entry(userTask)
                    .Collection(u => u.SubTasks).LoadAsync();

                await Task.WhenAll(attachmentsTask, labelItemsTask, notesTask, subTasks);
            }

            return userTask;
        }

        public UserTask Update(UserTask userTask)
        {
            return _context.UserTasks.Update(userTask).Entity;
        }

        public void Remove(UserTask userTask)
        {
            _context.UserTasks.Remove(userTask);
        }
    }
}
