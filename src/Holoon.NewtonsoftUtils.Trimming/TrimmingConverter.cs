using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Holoon.NewtonsoftUtils.Trimming
{
    public class TrimmingConverter : JsonConverter
    {
        public TrimmingOption ReadJsonTrimmingOption { get; set; }
        public TrimmingOption WriteJsonTrimmingOption { get; set; }
        public TrimmingConverter(TrimmingOption readJsonTrimmingOption, TrimmingOption writeJsonTrimmingOption)
        {
            WriteJsonTrimmingOption = writeJsonTrimmingOption;
            ReadJsonTrimmingOption = readJsonTrimmingOption;
        }
        public override bool CanRead => true;
        public override bool CanWrite => true;
        public override bool CanConvert(Type objectType) => objectType == typeof(SpacedString) || objectType == typeof(string);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var rValue = reader.Value;
            if (rValue is not string and not SpacedString)
                return rValue; // NOTE: reader.Value can be different of objectType if the model has changed between serialization and deserialization. 

            var value = (string)reader.Value;

            return objectType == typeof(SpacedString) ? (SpacedString)value : 

                Trim(value, ReadJsonTrimmingOption);
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string castedValue = value as SpacedString ?? Trim((string)value, WriteJsonTrimmingOption);
            var token = castedValue == null ? JValue.CreateNull() : JToken.FromObject(castedValue);
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
