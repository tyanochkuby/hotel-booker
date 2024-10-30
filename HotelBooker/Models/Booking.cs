using HotelBooker.Helpers;
using Newtonsoft.Json;

namespace HotelBooker.Models
{
    public class Booking
    {
        [JsonProperty("hotelId")]
        [JsonRequired]
        public string HotelId { get; set; }

        [JsonProperty("arrival")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        [JsonRequired]
        public DateTime Arrival { get; set; }

        [JsonProperty("departure")]
        [JsonRequired]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Departure { get; set; }

        [JsonProperty("roomType")]
        [JsonRequired]
        public string RoomType { get; set; }

        [JsonProperty("roomRate")]
        public string RoomRate { get; set; }
    }
}
