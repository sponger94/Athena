using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands.Label
{
    [DataContract]
    public class RemoveLabelCommand : IRequest<bool>
    {
        [DataMember]
        public int LabelId { get; private set; }

        public RemoveLabelCommand(int labelId)
        {
            LabelId = labelId;
        }
    }
}
