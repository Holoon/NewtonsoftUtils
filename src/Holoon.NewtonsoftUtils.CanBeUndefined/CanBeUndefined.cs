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

        public override string ToString() => Value.ToString();
        public override bool Equals(object obj) => Value.Equals(obj);
        public override int GetHashCode() => Value.GetHashCode();
        public object GetValueOrDefault() => Value;
    }
    public static class CanBeUndefined
    {
        public static CanBeUndefined<T> Create<T>(T value) => new(value, false);
    }
}
