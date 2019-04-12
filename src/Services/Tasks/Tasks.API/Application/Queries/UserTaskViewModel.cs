using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks.API.Application.Queries
{
    public class Attachment
    {
        public string uri { get; set; }
    }

    public class Label
    {
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
        public int tasknumber { get; set; }
        public DateTime datecreated { get; set; }
        public string taskname { get; set; }
        public bool iscompleted { get; set; }
        public List<Attachment> attachments { get; set; }
        public List<Label> labels { get; set; }
        public List<Note> notes { get; set; }
        public List<SubTask> subtasks { get; set; }
    }
}
