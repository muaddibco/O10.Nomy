using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using O10.Nomy.ExtensionMethods;
using O10.Nomy.Rapyd.DTOs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace O10.Nomy.Utils
{
    public class RapydConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.GetCustomAttribute<JsonConverterAttribute>()?.ConverterType == typeof(RapydConverter);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject? jobj = serializer.Deserialize<JObject>(reader);

            object? value = Activator.CreateInstance(objectType);

            var props = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var prop in props)
            {
                var tokenName = prop.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? prop.Name.ToUnderscoreDelimited().ToLower();

                if(jobj.Remove(tokenName, out var token))
                {
                    if(token.Type != JTokenType.Null)
                    {
                        prop.SetValue(value, token?.ToObject(prop.PropertyType));
                    }
                }
            }

            if(jobj.Count > 0 && value is PropertiesBaseDTO properties)
            {
                properties.Properties = new Dictionary<string, string>();
                foreach (var item in jobj)
                {
                    properties.Properties.Add(item.Key, item.Value.Value<string>());
                }
            }

            return value;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var propsObj = value as PropertiesBaseDTO;

            var props = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            writer.WriteStartObject();

            foreach (var prop in props)
            {
                var val = prop.GetValue(value);
                if (val != null)
                {
                    writer.WritePropertyName(prop.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName
                                             ?? prop.Name.ToUnderscoreDelimited().ToLower());

                    if(prop.PropertyType.IsEnum)
                    {
                        var member = prop.PropertyType.GetMember(val.ToString())[0];
                        var attr = (EnumMemberAttribute)member.GetCustomAttribute(typeof(EnumMemberAttribute));
                        writer.WriteValue(attr.Value ?? prop.GetValue(value));
                    }
                    else
                    {
                        serializer.Serialize(writer, prop.GetValue(value));
                    }
                }
            }

            if (propsObj != null)
            {
                foreach (var item in propsObj.Properties)
                {
                    writer.WritePropertyName(item.Key);
                    writer.WriteValue(item.Value);
                }
            }

            writer.WriteEndObject();
        }
    }
}
