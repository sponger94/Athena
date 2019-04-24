using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands
{
    [DataContract]
    public class RemoveNoteCommand : IRequest<bool>
    {
        [DataMember]
        public string Content { get; private set; }

        [DataMember]
        public int UserTaskId { get; private set; }

        public RemoveNoteCommand(string content, int userTaskId)
        {
            Content = content;
            UserTaskId = userTaskId;
        }
    }
}
