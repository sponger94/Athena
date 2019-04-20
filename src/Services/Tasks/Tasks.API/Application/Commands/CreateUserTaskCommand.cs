using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands
{
    [DataContract]
    public class CreateUserTaskCommand : IRequest<bool>
    {
        [DataMember]
        public string Name { get; private set; }

        public CreateUserTaskCommand(string name)
        {
            Name = name;
        }
    }
}
