using System;

namespace Athena.Pomodoro.API.Infrastructure.Exceptions
{
    public class PomodoroDomainException : Exception
    {
        public PomodoroDomainException()
        { }

        public PomodoroDomainException(string message)
            : base(message)
        { }

        public PomodoroDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
