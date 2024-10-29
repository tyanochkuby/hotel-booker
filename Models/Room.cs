using System.Text.Json.Serialization;

namespace HotelBooker.Models
{
    public class Room
    {
        [JsonPropertyName("roomType")]
        public string RoomType { get; set; }

        [JsonPropertyName("roomId")]
        public string RoomId { get; set; }
    }

}
