using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands.UserTask
{
    [DataContract]
    public class RemoveSubTaskCommand : IRequest<bool>
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool IsCompleted { get; private set; }

        [DataMember]
        public int UserTaskId { get; private set; }

        public RemoveSubTaskCommand(string name, bool isCompleted, int userTaskId)
        {
            Name = name;
            IsCompleted = isCompleted;
            UserTaskId = userTaskId;
        }
    }
}
