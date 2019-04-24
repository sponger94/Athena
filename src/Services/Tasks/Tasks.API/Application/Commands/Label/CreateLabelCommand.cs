using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using MediatR;

namespace Tasks.API.Application.Commands.Label
{
    [DataContract]
    public class CreateLabelCommand : IRequest<bool>
    {
        [DataMember]
        public int Argb { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        public CreateLabelCommand(int argb, string name)
        {
            Argb = argb;
            Name = name;
        }
    }
}
