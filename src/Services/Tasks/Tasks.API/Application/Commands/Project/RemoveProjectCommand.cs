using System.Runtime.Serialization;
using MediatR;

namespace Tasks.API.Application.Commands.Project
{
    [DataContract]
    public class RemoveProjectCommand : IRequest<bool>
    {
        [DataMember]
        public int ProjectId { get; private set; }

        public RemoveProjectCommand(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
