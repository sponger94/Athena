using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using MediatR;

namespace Tasks.API.Application.Commands
{
    [DataContract]
    public class CreateProjectCommand : IRequest<bool>
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public int Argb { get; private set; }

        public CreateProjectCommand(string name, int argb)
        {
            Name = name;
            Argb = argb;
        }
    }
}
