namespace DomainObject
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    /// <summary>
    /// An immutable object. 
    /// </summary>
    public class ComparableValue<T> : Value<T>, IComparable<ComparableValue<T>>, IComparable where T : IComparable<T>
    {
        private readonly T _value;

        protected ComparableValue(T value) : base(value)
        {
            _value = value;
        }

        public static bool operator <(ComparableValue<T> left, ComparableValue<T> right)
        {
            return left._value.CompareTo(right._value) < 0;
        }

        public static bool operator >(ComparableValue<T> left, ComparableValue<T> right)
        {
            return !(left < right) && left != right;
        }

        public static bool operator <=(ComparableValue<T> left, ComparableValue<T> right)
        {
            return left < right || left == right;
        }

        public static bool operator >=(ComparableValue<T> left, ComparableValue<T> right)
        {
            return left > right || left == right;
        }

        public static bool operator ==(ComparableValue<T> left, ComparableValue<T> right)
        {
            return left == right;
        }

        public static bool operator !=(ComparableValue<T> left, ComparableValue<T> right)
        {
            return left != right;
        }

        public int CompareTo(ComparableValue<T> other)
        {
            var comparable = this._value as IComparable;
            if (comparable == null)
            {
                throw new NotSupportedException("Type _value must derive from IComparable to be able to compare it.");
            }

            return comparable.CompareTo(other._value);
        }

        public int CompareTo(object obj)
        {
            var other = obj as ComparableValue<T>;
            if (other == null)
            {
                throw new NotSupportedException($"Cannot compare {obj} to {this.GetType()}");
            }

            return CompareTo(other);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
