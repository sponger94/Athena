using System;

namespace Goals.Domain.Exceptions
{
    public class GoalsDomainException : Exception
    {
        public GoalsDomainException()
        { }

        public GoalsDomainException(string message)
            : base(message)
        { }

        public GoalsDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
