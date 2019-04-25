using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands.UserTask
{
    [DataContract]
    public class AddAttachmentCommand : IRequest<bool>
    {
        [DataMember]
        public string Uri { get; private set; }

        [DataMember]
        public int UserTaskId { get; private set; }

        public AddAttachmentCommand(string uri, int userTaskId)
        {
            Uri = uri;
            UserTaskId = userTaskId;
        }
    }
}
