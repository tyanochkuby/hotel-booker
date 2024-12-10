using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HotelBooker.Helpers;

/// <summary>
/// Custom DateTime converter for JSON serialization
/// </summary>
public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    /// <summary>
    /// The date format
    /// </summary>
    private const string Format = "yyyyMMdd";

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString(Format));
    }

    /// <inheritdoc />
    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        return DateTime.ParseExact((string)reader.Value, Format, null);
    }
}