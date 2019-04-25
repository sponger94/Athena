using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands.UserTask
{
    [DataContract]
    public class UpdateUserTaskCommand : IRequest<bool>
    {
        [DataMember]
        public int UserTaskId { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public bool IsCompleted { get; private set; }

        [DataMember] 
        public int ProjectId { get; private set; }

        public UpdateUserTaskCommand(int userTaskId, string name, bool isCompleted)
        {
            UserTaskId = userTaskId;
            Name = name;
            IsCompleted = isCompleted;
        }
    }
}
