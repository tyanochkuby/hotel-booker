using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HotelBooker.Helpers
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private const string Format = "yyyyMMdd";

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(Format));
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return DateTime.ParseExact((string)reader.Value, Format, null);
        }
    }
}