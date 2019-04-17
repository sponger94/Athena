using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Domain.SeedWork
{
    public interface IAtomicValuesGettable
    {
        IEnumerable<object> GetAtomicValues();
    }
}
