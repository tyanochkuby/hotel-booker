using Newtonsoft.Json;

namespace HotelBooker.Models;

/// <summary>
/// Represents a room type
/// </summary>
public class RoomType
{
    /// <summary>
    /// The room type's code
    /// </summary>
    [JsonProperty("code")]
    [JsonRequired]
    public string Code { get; set; }


    /// <summary>
    /// The room type's description
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }
}
