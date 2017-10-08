using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Transdirect.Converter
{
    public class LowercaseStringEnumConverter: StringEnumConverter
    {
        
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            Enum e = (Enum)value;

            string enumName = e.ToString("G");

            if (char.IsNumber(enumName[0]) || enumName[0] == '-')
            {
                if (!AllowIntegerValues)
                {
                    throw new JsonSerializationException($"Integer value {enumName} is not allowed");
                }

                // enum value has no name so write number
                writer.WriteValue(value);
            }
            else
            {
                Type enumType = e.GetType();

                string finalName = Enum.GetName(enumType, e).ToLower();

                writer.WriteValue(finalName);
            }
        }
    }
}