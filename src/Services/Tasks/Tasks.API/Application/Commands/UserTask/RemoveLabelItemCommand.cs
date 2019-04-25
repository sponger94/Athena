using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands.UserTask
{
    [DataContract]
    public class RemoveLabelItemCommand : IRequest<bool>
    {
        [DataMember]
        public int UserTaskId{ get; private set; }

        [DataMember]
        public int LabelId { get; private set; }

        public RemoveLabelItemCommand(int userTaskId, int labelId)
        {
            UserTaskId = userTaskId;
            LabelId = labelId;
        }
    }
}
