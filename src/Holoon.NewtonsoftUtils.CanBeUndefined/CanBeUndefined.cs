﻿namespace Holoon.NewtonsoftUtils.CanBeUndefined;

public sealed class CanBeUndefined<T> : ICanBeUndefined
{
    internal CanBeUndefined(T value, bool isUndefined)
    {
        IsUndefined = isUndefined;
        Value = value;
    }
    public T Value { get; }
    public bool IsUndefined { get; }

    public static implicit operator T(CanBeUndefined<T> value) => value.Value;
    public static implicit operator CanBeUndefined<T>(T value) => new(value, false);
    public static implicit operator CanBeUndefined<T>(Undefined _) => new(default, true);

    public override string ToString() => Value?.ToString();
    public override bool Equals(object obj)
    {
#pragma warning disable IDE0046 // Use conditional expression for return
        if (obj != null && Value == null)
            return obj is CanBeUndefined<T> other && other.Value == null && IsUndefined == other.IsUndefined;

        if (obj is CanBeUndefined<T> otherObj)
            return Value?.Equals(otherObj.Value) ?? otherObj.Value?.Equals(Value) ?? true;

        return Value?.Equals(obj) ?? obj?.Equals(Value) ?? true;
#pragma warning restore IDE0046 // Use conditional expression for return
    }
    public override int GetHashCode() => Value?.GetHashCode() ?? default;
    public object GetValueOrDefault() => Value;
}
