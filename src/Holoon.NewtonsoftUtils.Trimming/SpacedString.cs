namespace System;

public class SpacedString
{
    private readonly string _InternalValue = null;
    private SpacedString(string internalValue) => _InternalValue = internalValue;

    public static implicit operator string(SpacedString value) => value?._InternalValue;
    public static implicit operator SpacedString(string value) => new(value);

    public override string ToString() => _InternalValue;
}