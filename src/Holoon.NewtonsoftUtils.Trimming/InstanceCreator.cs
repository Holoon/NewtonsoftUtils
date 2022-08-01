using System;

namespace Holoon.NewtonsoftUtils.Trimming;
public static class InstanceCreator
{
    public static object Create(Type type)
    {
        if (type == null)
            return null;

        try
        {
            return Activator.CreateInstance(type, true);
        }
        catch
        {
            return System.Runtime.Serialization.FormatterServices.GetUninitializedObject(type);
        }
    }
}
