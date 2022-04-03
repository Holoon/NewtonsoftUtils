namespace Holoon.NewtonsoftUtils.CanBeUndefined
{

    public class Undefined
    {
        public static Undefined Value { get; } = new();
        private Undefined() { }
    }


    //NOTE: Name proposal to echo Nullable : Undefinable<T>, Ignorable<T>, JSONIgnorable<T>, Undefined<T>
    public class CanBeUndefined<T> : ICanBeUndefined
    {
        //public static CanBeUndefined<T> Undefined => new(default, true);
        
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

        public override string ToString() => Value.ToString();
        public override bool Equals(object obj) => Value.Equals(obj);
        public override int GetHashCode() => Value.GetHashCode();
        public object GetValueOrDefault() => Value;
    }


    public static class CanBeUndefined
    {
        //NOTE: What For ? I suggest deletion
        public static CanBeUndefined<T> Create<T>(T value) => new(value, false);
    }

    
}
