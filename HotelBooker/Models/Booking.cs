using HotelBooker.Helpers;
using System.Text.Json.Serialization;

namespace HotelBooker.Models
{
    public class Booking
    {
        [JsonPropertyName("hotelId")]
        public string HotelId { get; set; }

        [JsonPropertyName("arrival")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Arrival { get; set; }

        [JsonPropertyName("departure")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Departure { get; set; }

        [JsonPropertyName("roomType")]
        public string RoomType { get; set; }

        [JsonPropertyName("roomRate")]
        public string RoomRate { get; set; }
    }
}
