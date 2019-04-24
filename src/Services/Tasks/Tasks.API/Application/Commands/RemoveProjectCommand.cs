using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using MediatR;

namespace Tasks.API.Application.Commands
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
