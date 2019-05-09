using Athena.Pomodoros.API.Model;
using FluentValidation;
using System;

namespace Athena.Pomodoros.API.Validations
{
    public class PomodoroValidator : AbstractValidator<Pomodoro>
    {
        public PomodoroValidator()
        {
            RuleFor(p => p.ProjectId).GreaterThan(0);
            RuleFor(p => p.ProjectName).NotEmpty().Length(32);
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.Duration).GreaterThan(TimeSpan.FromMinutes(5));
        }
    }
}
