namespace Holoon.NewtonsoftUtils.CanBeUndefined
{
    public class CanBeUndefined<T> : ICanBeUndefined
    {
        internal CanBeUndefined(T value, bool isUndefined)
        {
            IsUndefined = isUndefined;
            Value = value;
        }
        public T Value { get; }
        public bool IsUndefined { get; }

        public static explicit operator T(CanBeUndefined<T> value) => value.Value;
        public static implicit operator CanBeUndefined<T>(T value) => new(value, false);
        public static implicit operator CanBeUndefined<T>(Undefined _) => new(default, true);

        public override string ToString() => Value?.ToString();
        public override bool Equals(object obj)
        {
#pragma warning disable IDE0046 // Use conditional expression for return
            if (obj != null && Value == null)
                return obj is CanBeUndefined<T> other && other.Value == null && IsUndefined == other.IsUndefined;

            return Value?.Equals(obj) ?? obj?.Equals(Value) ?? true;
#pragma warning restore IDE0046 // Use conditional expression for return
        }
        public override int GetHashCode() => Value?.GetHashCode() ?? default;
        public object GetValueOrDefault() => Value;
    }
    public static class CanBeUndefined
    {
        public static CanBeUndefined<T> Create<T>(T value) => new(value, false);
    }
}
