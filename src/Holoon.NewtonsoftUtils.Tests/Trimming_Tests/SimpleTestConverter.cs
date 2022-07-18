using Newtonsoft.Json;
using System;

namespace Holoon.NewtonsoftUtils.Tests.Trimming_Tests;

public class SimpleTestConverter : JsonConverter
{
    public const string ADDING = "*ADD*";
    public override bool CanRead => false; 
    public override bool CanWrite => true; 
    public override bool CanConvert(Type objectType) => objectType == typeof(string);
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return null;
    }
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString() + ADDING);
    }
}
