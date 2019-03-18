using System;
using System.Collections.Generic;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.TasksAggregate
{
    public class Task : Entity, IAggregateRoot
    {
        public DateTime DateCreated { get; private set; }

        public string Name { get; private set; }

        public bool IsCompleted { get; private set; }

        public int? ProjectId { get; private set; }

        private List<Attachment> _attachments;
        public IReadOnlyCollection<Attachment> Attachments => _attachments;

        private List<TaskLabelItem> _labelItems;
        public IReadOnlyCollection<TaskLabelItem> LabelItems => _labelItems;

        private List<Note> _notes;
        public IReadOnlyCollection<Note> Notes => _notes;

        private List<SubTask> _subTasks;
        public IReadOnlyCollection<SubTask> SubTasks => _subTasks;

        private Task()
        {
            _attachments = new List<Attachment>();
            _labelItems = new List<TaskLabelItem>();
            _notes = new List<Note>();
            _subTasks = new List<SubTask>();
        }

        public Task(string name)
            : this()
        {
            Name = name;
        }

        public Task(string name, int projectId)
            : this(name)
        {
            ProjectId = projectId;
        }


        public void AddAttachment(Attachment attachment)
        {
            _attachments.Add(attachment);
        }

        public void RemoveAttachment(Attachment attachment)
        {
            _attachments.Remove(attachment);
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
    }
}
