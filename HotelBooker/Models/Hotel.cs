using HotelBooker.Interfaces;
using Newtonsoft.Json;

namespace HotelBooker.Models
{
    public class Hotel
    {
        [JsonProperty("id")]
        [JsonRequired]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("roomTypes")]
        [JsonRequired]
        public List<RoomType> RoomTypes { get; set; }

        [JsonProperty("rooms")]
        [JsonRequired]
        public List<Room> Rooms { get; set; }
    }
}

