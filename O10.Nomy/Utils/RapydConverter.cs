using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using O10.Nomy.ExtensionMethods;
using O10.Nomy.Rapyd.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace O10.Nomy.Utils
{
    public class RapydConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject? jobj = serializer.Deserialize<JObject>(reader);

            object? value = Activator.CreateInstance(objectType);

            var props = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var prop in props)
            {
                var tokenName = prop.Name.ToUnderscoreDelimited().ToLower();
                if(jobj.Remove(tokenName, out var token))
                {
                    prop.SetValue(value, token?.ToObject(prop.PropertyType));
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
                    writer.WritePropertyName(prop.Name.ToUnderscoreDelimited().ToLower());
                    writer.WriteValue(prop.GetValue(value));
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
