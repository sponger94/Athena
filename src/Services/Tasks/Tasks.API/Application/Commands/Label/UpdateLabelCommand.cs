using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands.Label
{
    [DataContract]
    public class UpdateLabelCommand : IRequest<bool>
    {
        [DataMember]
        public int LabelId { get; private set; }

        [DataMember]
        public int Argb { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        public UpdateLabelCommand(int labelId, int argb, string name)
        {
            LabelId = labelId;
            Argb = argb;
            Name = name;
        }
    }
}
