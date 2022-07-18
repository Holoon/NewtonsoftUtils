namespace Holoon.NewtonsoftUtils.CanBeUndefined;

internal interface ICanBeUndefined
{
    bool IsUndefined { get; }
    public bool IsDefined => !IsUndefined;
    object GetValueOrDefault();
}
