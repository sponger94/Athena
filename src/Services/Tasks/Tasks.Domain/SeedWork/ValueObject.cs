using System.Collections.Generic;
using System.Linq;

namespace Tasks.Domain.SeedWork
{
    public abstract class ValueObject : IAtomicValuesGettable
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        public abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            return new ValueObjectEqualityComparer().Equals(this, obj as IAtomicValuesGettable);
        }

        public override int GetHashCode()
        {
            return new ValueObjectEqualityComparer().GetHashCode(this);
        }

        public ValueObject GetCopy()
        {
            return this.MemberwiseClone() as ValueObject;
        }
    }
}
