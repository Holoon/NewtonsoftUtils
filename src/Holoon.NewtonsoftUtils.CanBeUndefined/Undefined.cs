namespace Holoon.NewtonsoftUtils.CanBeUndefined;

public class Undefined
{
    private Undefined() { }
    public static Undefined Value { get; } = new();
}
