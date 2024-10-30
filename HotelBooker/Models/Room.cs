

using Newtonsoft.Json;

namespace HotelBooker.Models
{
    public class Room
    {
        [JsonProperty("roomType")]
        [JsonRequired]
        public string RoomType { get; set; }

        [JsonProperty("roomId")]
        [JsonRequired]
        public string RoomId { get; set; }
    }

}
