namespace Holoon.NewtonsoftUtils.CanBeUndefined
{
    internal interface ICanBeUndefined
    {
        bool IsUndefined { get; }
        object GetValueOrDefault();
    }
}
