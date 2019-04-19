using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Domain.SeedWork;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace Tasks.API.Application.Queries
{
    public class Attachment
    {
        public string uri { get; set; }
    }

    public class Label
    {
        public int labelid { get; set; }
        public int argb { get; set; }
        public string labelname { get; set; }
    }

    public class Note
    {
        public string content { get; set; }
    }

    public class SubTask
    {
        public string subtaskname { get; set; }
        public bool iscompleted { get; set; }
    }

    public class UserTask
    {
        public DateTime datecreated { get; set; }
        public string taskname { get; set; }
        public bool iscompleted { get; set; }
        public List<Attachment> attachments { get; set; }
        public List<Label> labels { get; set; }
        public List<Note> notes { get; set; }
        public List<SubTask> subtasks { get; set; }

        public UserTask()
        {
            attachments = new List<Attachment>();
            labels = new List<Label>();
            notes = new List<Note>();
            subtasks = new List<SubTask>();
        }
    }

    public class UserTaskSummary
    {
        public DateTime datecreated { get; set; }
        public string taskname { get; set; }
        public bool iscompleted { get; set; }
    }

    public class ProjectSummary
    {
        public int argb { get; set; }
        public string name { get; set; }
    }
}
