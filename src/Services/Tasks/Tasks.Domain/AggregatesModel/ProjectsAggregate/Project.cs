using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Tasks.Domain.Exceptions;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.ProjectsAggregate
{
    public class Project : Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }

        public string Name { get; private set; }

        public int Argb { get; private set; }

        [NotMapped]
        public Color Color { get; private set; }

        private Project()
        { }

        public Project(string identityGuid, string name)
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identityGuid) 
                ? identityGuid 
                : throw new ArgumentNullException(nameof(identityGuid));
            SetName(name);
        }

        public Project(string identityGuid, string name, Color color)
            : this(identityGuid, name)
        {
            SetColor(color);
        }

        public void SetName(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name) 
                ? name
                : throw new ArgumentNullException(nameof(name));
        }

        public void SetColor(Color color)
        {
            Color = color;
            Argb = color.ToArgb();
        }
    }
}
