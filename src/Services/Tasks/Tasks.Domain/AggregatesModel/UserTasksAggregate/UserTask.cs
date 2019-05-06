using System;
using System.Collections.Generic;
using System.Linq;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.UserTasksAggregate
{
    public class UserTask : Entity, IAggregateRoot
    {
        public DateTime DateCreated { get; private set; }

        public string Name { get; private set; }

        public bool IsCompleted { get; private set; }

        public int ProjectId { get; private set; }

        private List<Attachment> _attachments;
        public IReadOnlyCollection<Attachment> Attachments => _attachments;

        private List<UserTaskLabelItem> _labelItems;
        public IReadOnlyCollection<UserTaskLabelItem> LabelItems => _labelItems;

        private List<Note> _notes;
        public IReadOnlyCollection<Note> Notes => _notes;

        private List<SubTask> _subTasks;
        public IReadOnlyCollection<SubTask> SubTasks => _subTasks;

        private UserTask()
        {
            _attachments = new List<Attachment>();
            _labelItems = new List<UserTaskLabelItem>();
            _notes = new List<Note>();
            _subTasks = new List<SubTask>();

            DateCreated = DateTime.Now;
        }

        public UserTask(string name)
            : this()
        {
            Name = !string.IsNullOrEmpty(name) 
                ? name 
                : throw new ArgumentNullException(nameof(name));
        }

        public UserTask(string name, int projectId)
            : this(name)
        {
            ProjectId = projectId > 0 
                ? projectId 
                : throw new ArgumentException("The given projectId can not be less than or equal to 0.");
        }


        public void AddAttachment(Attachment attachment)
        {
            _attachments.Add(attachment);
        }

        public void RemoveAttachment(Attachment attachment)
        {
            _attachments.Remove(attachment);
        }

        public void AddLabel(Label label)
        {
            _labelItems.Add(new UserTaskLabelItem(label.Id));
        }

        public void RemoveLabel(Label label)
        {
            var labelItem = _labelItems.FirstOrDefault(li => li.LabelId == label.Id);
            _labelItems.Remove(labelItem);
        }

        public void AddNote(Note note)
        {
            _notes.Add(note);
        }

        public void RemoveNote(Note note)
        {
            _notes.Remove(note);
        }

        public void AddSubTask(SubTask subTask)
        {
            _subTasks.Add(subTask);
        }

        public void RemoveSubTask(SubTask subTask)
        {
            _subTasks.Remove(subTask);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetIsCompleted(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }

        public void SetProjectId(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
