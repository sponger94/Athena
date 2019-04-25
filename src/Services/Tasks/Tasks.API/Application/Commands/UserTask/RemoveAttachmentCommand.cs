using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands.UserTask
{
    [DataContract]
    public class RemoveAttachmentCommand : IRequest<bool>
    {
        [DataMember]
        public string Uri { get; private set; }

        [DataMember]
        public int UserTaskId { get; private set; }

        public RemoveAttachmentCommand(string uri, int userTaskId)
        {
            Uri = uri;
            UserTaskId = userTaskId;
        }
    }
}
