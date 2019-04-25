using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands.Project
{
    [DataContract]
    public class UpdateProjectCommand : IRequest<bool>
    {
        [DataMember]
        public int ProjectId { get; private set; }

        [DataMember]
        public int Argb { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        public UpdateProjectCommand(int projectId, int argb, string name)
        {
            ProjectId = projectId;
            Argb = argb;
            Name = name;
        }
    }
}
