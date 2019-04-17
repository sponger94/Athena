using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks.Domain.SeedWork
{
    public class ValueObjectEqualityComparer : IEqualityComparer<IAtomicValuesGettable>
    {
        public bool Equals(IAtomicValuesGettable x, IAtomicValuesGettable y)
        {
            if (x == null || y == null || x.GetType() != y.GetType())
                return false;

            using (IEnumerator<object> xValues = x.GetAtomicValues().GetEnumerator())
            {
                using (IEnumerator<object> yValues = y.GetAtomicValues().GetEnumerator())
                {
                    while (xValues.MoveNext() && yValues.MoveNext())
                    {
                        if (ReferenceEquals(xValues.Current, null) ^ ReferenceEquals(yValues.Current, null))
                        {
                            return false;
                        }
                        if (xValues.Current != null && !xValues.Current.Equals(yValues.Current))
                        {
                            return false;
                        }
                    }
                    return !xValues.MoveNext() && !yValues.MoveNext();
                }
            }
        }

        public int GetHashCode(IAtomicValuesGettable obj)
        {
            return obj.GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
