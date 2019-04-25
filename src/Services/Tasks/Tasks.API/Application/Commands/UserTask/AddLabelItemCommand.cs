using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands.UserTask
{
    [DataContract]
    public class AddLabelItemCommand : IRequest<bool>
    {
        [DataMember]
        public int UserTaskId { get; private set; }

        [DataMember]
        public int LabelId { get; private set; }

        [DataMember]
        public int Argb { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        public AddLabelItemCommand(int labelId, int argb, string name)
        {
            LabelId = labelId;
            Argb = argb;
            Name = name;
        }
    }
}
