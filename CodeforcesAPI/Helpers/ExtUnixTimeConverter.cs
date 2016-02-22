using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CodeforcesAPI.Helpers
{
    public class ExtUnixTimeConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime)
            {
                writer.WriteValue(((DateTime)value).ToUnixTime());
            }
            else
            {
                throw new Exception("Expected date object value.");
            }
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ((long)reader.Value).ToDateTime();
        }
    }
}
