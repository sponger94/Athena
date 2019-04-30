using System;

namespace Athena.Pomodoro.API.Model
{
    public class Pomodoro
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime Time { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
