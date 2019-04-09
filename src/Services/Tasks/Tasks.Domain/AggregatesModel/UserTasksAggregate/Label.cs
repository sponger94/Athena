using System.Drawing;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.AggregatesModel.UserTasksAggregate
{
    public class Label : Entity
    {
        public Color Color { get; private set; }

        public string Name { get; private set; }

        private Label()
        { }

        public Label(Color color, string name)
        {
            Color = color;
            Name = name;
        }
    }
}
