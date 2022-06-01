using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Holoon.NewtonsoftUtils.CanBeUndefined
{
    public class CanBeUndefinedResolver : DefaultContractResolver
    {
        private static readonly Dictionary<Type, Type> _EnumerableTypeCache = new();
        private static readonly Dictionary<Type, ConstructorInfo> _ConstructorCache = new();
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property?.PropertyType != null && property.PropertyType.IsAssignableTo(typeof(ICanBeUndefined)))
            {
                property.DefaultValueHandling = DefaultValueHandling.Populate;
                property.DefaultValue = CreateInstanceOf(property.PropertyType, null, true);
                property.ShouldDeserialize = instance => true;
                property.ShouldSerialize = instance => 
                {
                    var canBeUndefined = property?.ValueProvider?.GetValue(instance) as ICanBeUndefined;
                    return !canBeUndefined?.IsUndefined ?? true;
                };
                property.Converter = new CanBeUndefinedConverter();
            }
            if (property?.PropertyType != null && IsEnumerableOfCanBeUndefined(property.PropertyType))
            {
                property.ItemConverter = new CanBeUndefinedConverter();
                property.Converter = new CanBeUndefinedCollectionConverter();
            }

            return property; 
        }
        private static Type GetEnumerableElementType(Type type)
        {
            static Type GetEnumerableType(Type i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>) ? i.GenericTypeArguments[0] : null;

            if (_EnumerableTypeCache.ContainsKey(type))
                return _EnumerableTypeCache[type];

            var enumerableType = type.IsArray ? type.GetElementType() :
                   GetEnumerableType(type) ?? type.GetInterfaces().Select(i => GetEnumerableType(i)).FirstOrDefault(t => t != null);

            _EnumerableTypeCache.Add(type, enumerableType);
            return enumerableType;
        }
        private static bool IsEnumerableOfCanBeUndefined(Type type) => GetEnumerableElementType(type)?.IsAssignableTo(typeof(ICanBeUndefined)) ?? false;
        private static IEnumerable<object> GetFilteredValues(object values)
        {
            return (values as System.Collections.IEnumerable)
                ?.OfType<ICanBeUndefined>()
                ?.Where(o => !o.IsUndefined)
                ?.Select(o => o.GetValueOrDefault());
        }
        private static object CreateInstanceOf(Type objectType, object value, bool isUndefined = false)
        {
            ConstructorInfo constructor;
            if (_ConstructorCache.ContainsKey(objectType))
            {
                constructor = _ConstructorCache[objectType];
            }
            else
            {
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
                constructor = objectType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { objectType.GetGenericArguments()?[0], typeof(bool) }, null);
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
                _ConstructorCache.Add(objectType, constructor);
            }

            var instance = constructor?.Invoke(new object[] { value, isUndefined });
            return instance;
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
                if (IsEnumerableOfCanBeUndefined(realType))
                {
                    Type elementType = GetEnumerableElementType(realType);

                    var elementRealType = elementType.GetGenericArguments()?[0];
                    if (elementRealType == null)
                        return null;

                    var values = token.Select(t => t.ToObject(elementRealType, serializer));
                    var instances = values.Select(v => CreateInstanceOf(elementType, v)).ToList(); // TODO: Pas forcément une liste, peut être n'importe quel IEnumerable<CanBeUndefined<T>> ou un CanBeUndefined<T>[]
                    var instanceOfEnumerable = CreateInstanceOf(objectType, instances);
                    return instanceOfEnumerable;
                }
                
                var value = token.ToObject(realType, serializer);
                var instance = CreateInstanceOf(objectType, value);
                return instance;
            }
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value is not ICanBeUndefined canBeUndefined)
                    throw new ArgumentException($"{nameof(value)} is not of the expected type", nameof(value));

                var internalValue = canBeUndefined.GetValueOrDefault();

                if (IsEnumerableOfCanBeUndefined(internalValue.GetType()))
                    internalValue = GetFilteredValues(internalValue);

                var token = internalValue == null ? JValue.CreateNull() : JToken.FromObject(internalValue, serializer);
                token.WriteTo(writer);
                writer.Flush();
            }
        }
        private sealed class CanBeUndefinedCollectionConverter : JsonConverter
        {
            public override bool CanRead => false;
            public override bool CanWrite => true;
            public override bool CanConvert(Type objectType) => objectType != null && IsEnumerableOfCanBeUndefined(objectType);
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) 
                => throw new NotImplementedException("No need to use this converter when deserializing");
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var filteredValues = GetFilteredValues(value);

                var token = filteredValues == null ? JValue.CreateNull() : JToken.FromObject(filteredValues);
                token.WriteTo(writer);
                writer.Flush();
            }
        }
    }
}
