using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Domain.SeedWork;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace Tasks.API.Application.Queries
{
    public class Attachment : IAtomicValuesGettable
    {
        public string uri { get; set; }
        public int usertaskid { get; set; }

        public IEnumerable<object> GetAtomicValues()
        {
            yield return uri;
            yield return usertaskid;
        }
    }

    public class Label
    {
        public int labelid { get; set; }
        public int argb { get; set; }
        public string labelname { get; set; }
    }

    public class Note : IAtomicValuesGettable
    {
        public string content { get; set; }
        public int usertaskid { get; set; }

        public IEnumerable<object> GetAtomicValues()
        {
            yield return content;
            yield return usertaskid;
        }
    }

    public class SubTask : IAtomicValuesGettable
    {
        public string subtaskname { get; set; }
        public bool iscompleted { get; set; }
        public int usertaskid { get; set; }

        public IEnumerable<object> GetAtomicValues()
        {
            yield return subtaskname;
            yield return iscompleted;
            yield return usertaskid;
        }
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
