using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands
{
    [DataContract]
    public class AddSubTaskCommand : IRequest<bool>
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public int UserTaskId { get; private set; }

        public AddSubTaskCommand(string name, int userTaskId)
        {
            Name = name;
            UserTaskId = userTaskId;
        }
    }
}
