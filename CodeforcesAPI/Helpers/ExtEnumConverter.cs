using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CodeforcesAPI.Helpers;

namespace CodeforcesAPI.Helpers
{
    public class ExtEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType.BaseType == typeof(Enum))
            {
                var name = reader.Value.ToString().Replace("_", string.Empty).ToLower();
                foreach (var field in objectType.GetFields())
                {
                    if (field.Name.ToLower() == name)
                    {
                        return field.GetValue(null);
                    }
                }
            }

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}
