using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace Holoon.NewtonsoftUtils.CanBeUndefined
{
    public class CanBeUndefinedResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property?.PropertyType != null && property.PropertyType.IsAssignableTo(typeof(ICanBeUndefined)))
            {
                property.DefaultValueHandling = DefaultValueHandling.Populate;
                property.DefaultValue = CanBeUndefinedConverter.CreateInstanceOf(property.PropertyType, null, true);
                property.ShouldDeserialize = instance => true;
                property.ShouldSerialize = instance => 
                {
                    var canBeUndefined = property?.ValueProvider?.GetValue(instance) as ICanBeUndefined;
                    return !canBeUndefined?.IsUndefined ?? true;
                };
                property.Converter = new CanBeUndefinedConverter();
            }

            return property;
        }
        private sealed class CanBeUndefinedConverter : JsonConverter
        {
            public override bool CanRead => true;
            public override bool CanWrite => true;
            public override bool CanConvert(Type objectType) => objectType != null && objectType.IsAssignableTo(typeof(ICanBeUndefined));
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (objectType != null && !objectType.IsAssignableTo(typeof(ICanBeUndefined)))
                    throw new ArgumentException($"{nameof(objectType)} is not of the expected type", nameof(objectType));

                if (objectType == null)
                    return null;

                var realType = objectType.GetGenericArguments()?[0];
                if (realType == null)
                    return null;

                var token = JToken.Load(reader);
                var value = token.ToObject(realType, serializer);

                var instance = CreateInstanceOf(objectType, value);
                return instance;
            }
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value is not ICanBeUndefined canBeUndefined)
                    throw new ArgumentException($"{nameof(value)} is not of the expected type", nameof(value));

                var internalValue = canBeUndefined.GetValueOrDefault();
                var token = internalValue == null ? JValue.CreateNull() : JToken.FromObject(internalValue);
                token.WriteTo(writer);
                writer.Flush();
            }
            internal static object CreateInstanceOf(Type objectType, object value, bool isUndefined = false)
            {
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
                var constructor = objectType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { objectType.GetGenericArguments()?[0], typeof(bool) }, null);
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
                var instance = constructor?.Invoke(new object[] { value, isUndefined });
                return instance;
            }
        }
    }
}
