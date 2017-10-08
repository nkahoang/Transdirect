using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Transdirect.Converter
{
    public class DateOnlyConverter : DateTimeConverterBase
    {
        const string DateOnlyFormat = "yyyy-MM-dd";
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(reader.Value.ToString(), DateOnlyFormat, provider);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue( ((DateTime)value).ToString(DateOnlyFormat) );
        }
    }
}