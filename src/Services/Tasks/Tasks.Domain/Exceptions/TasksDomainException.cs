using System;

namespace Tasks.Domain.Exceptions
{
    public class TasksDomainException : Exception
    {
        public TasksDomainException()
        { }

        public TasksDomainException(string message)
            : base(message)
        { }

        public TasksDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
