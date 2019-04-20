using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands
{
    [DataContract]
    public class RemoveUserTaskCommand : IRequest<bool>
    {
        [DataMember]
        public int UserTaskId { get; private set; }

        public RemoveUserTaskCommand(int userTaskId)
        {
            UserTaskId = userTaskId;
        }
    }
}
