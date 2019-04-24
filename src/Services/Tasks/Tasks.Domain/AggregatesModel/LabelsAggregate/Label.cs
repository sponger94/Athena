using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.UserTasksAggregate
{
    public class Label : Entity, IAggregateRoot
    {
        public int Argb { get; private set; }

        [NotMapped]
        public Color Color { get; private set; }

        public string Name { get; private set; }

        private Label()
        { }

        public Label(string name, Color color)
        {
            SetName(name);
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
