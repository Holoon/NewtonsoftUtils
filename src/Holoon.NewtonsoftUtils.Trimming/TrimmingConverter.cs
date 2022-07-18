using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Holoon.NewtonsoftUtils.Trimming
{
    public class TrimmingConverter : JsonConverter
    {
        public TrimmingOption ReadJsonTrimmingOption { get; set; }
        public TrimmingOption WriteJsonTrimmingOption { get; set; }
        public StringPropertiesToNotTrimHandlerCollection StringPropertiesToNotTrim { get; }
        public TrimmingConverter(TrimmingOption readJsonTrimmingOption, TrimmingOption writeJsonTrimmingOption)
        {
            WriteJsonTrimmingOption = writeJsonTrimmingOption;
            ReadJsonTrimmingOption = readJsonTrimmingOption;
            StringPropertiesToNotTrim = new StringPropertiesToNotTrimHandlerCollection();
        }
        public override bool CanRead => ReadJsonTrimmingOption != TrimmingOption.NoTrim;
        public override bool CanWrite => true;
        private IEnumerable<PropertyInfo> GetPropertiesToTrim(Type objectType) => objectType.GetProperties()
            .Where(p => p.PropertyType == typeof(string)
            && !p.CustomAttributes.Any(a => a.AttributeType == typeof(SpacedStringAttribute))
            && !IsInStringPropertiesToIgnore(objectType, p)
            && p.SetMethod != null);
        private bool IsInStringPropertiesToIgnore(Type objectType, PropertyInfo pInfo) =>
            StringPropertiesToNotTrim._InternalListOfPropertiesToIgnore.Any(p => p.ObjectType == objectType && pInfo.Name == p.PropertyName);
        public override bool CanConvert(Type objectType) => GetPropertiesToTrim(objectType).Any() || objectType == typeof(SpacedString);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(SpacedString))
                return (SpacedString)(string)reader.Value;

            if (reader.TokenType == JsonToken.Null)
                return null;

            var value = existingValue ?? System.Runtime.Serialization.FormatterServices.GetUninitializedObject(objectType);
            serializer.Populate(reader, value);

            foreach (var propertyInfo in GetPropertiesToTrim(objectType))
            {
                if (propertyInfo.GetValue(value) is string propertyValue)
                    propertyInfo.SetValue(value, Trim(propertyValue, ReadJsonTrimmingOption));
            }
            return value;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is SpacedString spacedString)
            {
                JToken.FromObject((string)spacedString, serializer).WriteTo(writer);
                return;
            }

            if (value == null)
            {
                JValue.CreateNull().WriteTo(writer);
                return;
            }

            var type = value.GetType();
            foreach (var propertyInfo in GetPropertiesToTrim(type))
            {
                if (propertyInfo.GetValue(value) is string propertyValue)
                    propertyInfo.SetValue(value, Trim(propertyValue, WriteJsonTrimmingOption));
            }

            JToken token = InternalUtils.SerializeIgnoreCallerConverterFromObject(value, serializer);
            token.WriteTo(writer);
        }
        private static string Trim(string value, TrimmingOption option) => option switch
        {
            TrimmingOption.TrimBoth => value?.Trim(),
            TrimmingOption.TrimStart => value?.TrimStart(),
            TrimmingOption.TrimEnd => value?.TrimEnd(),
            TrimmingOption.NoTrim => value,
            _ => value,
        };
    }
}
