using Newtonsoft.Json;

namespace HotelBooker.Models
{
    public class RoomType
    {
        [JsonProperty("code")]
        [JsonRequired]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amenities")]
        public List<string> Amenities { get; set; }

        [JsonProperty("features")]
        public List<string> Features { get; set; }
    }

}
