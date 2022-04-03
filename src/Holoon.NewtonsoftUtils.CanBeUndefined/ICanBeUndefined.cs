namespace Holoon.NewtonsoftUtils.CanBeUndefined
{
    internal interface ICanBeUndefined
    {
        bool IsUndefined { get; }

        //NOTE: Added because myproperty.IsUndefined == false is difficult to understand (double negative)
        public bool IsDefined => !IsUndefined;

        object GetValueOrDefault();
    }
}
