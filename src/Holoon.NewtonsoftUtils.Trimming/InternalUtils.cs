using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Holoon.NewtonsoftUtils.Trimming;

// NOTE: Copied from https://stackoverflow.com/a/29720068/2546739
public class InternalUtils
{
    /// <summary>
    /// Use the serializer without the caller converter.
    /// </summary>
    /// <returns></returns>
    public static JToken SerializeIgnoreCallerConverterFromObject(object value, JsonSerializer serializer)
    {
        if (value == null)
            return JValue.CreateNull();

        var dto = Activator.CreateInstance(typeof(DefaultSerializationDto<>).MakeGenericType(value.GetType()), value);
        var root = JObject.FromObject(dto, serializer);
        return RemoveFromLowestPossibleParent(root["Value"]) ?? JValue.CreateNull();
    }
    private static JToken RemoveFromLowestPossibleParent(JToken node)
    {
        if (node == null)
            throw new ArgumentNullException(nameof(node));

        // NOTE: If the parent is a JProperty, remove that instead of the token itself.
        var contained = node.Parent is JProperty ? node.Parent : node;
        contained.Remove();
        // NOTE: Also detach the node from its immediate containing property -- Remove() does not do this even though it seems like it should
        if (contained is JProperty)
            ((JProperty)node.Parent).Value = null;
        return node;
    }
    private interface IHasValue
    {
        object GetValue();
    }

    [JsonObject(NamingStrategyType = typeof(Newtonsoft.Json.Serialization.DefaultNamingStrategy), IsReference = false)]
    private sealed class DefaultSerializationDto<T> : IHasValue
    {
#pragma warning disable S1144 // Unused private types or members should be removed - Used in reflexion. 
        public DefaultSerializationDto(T value) { this.Value = value; }
#pragma warning restore S1144 // Unused private types or members should be removed
        public DefaultSerializationDto() { }

        [JsonConverter(typeof(NoConverter)), JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Serialize)]
        public T Value { get; set; }
        object IHasValue.GetValue() { return Value; }
    }
    private sealed class NoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) { throw new NotImplementedException(); }
        public override bool CanRead { get { return false; } }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) { throw new NotImplementedException(); }
        public override bool CanWrite { get { return false; } }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { throw new NotImplementedException(); }
    }
}
