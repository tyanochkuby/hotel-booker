using HotelBooker.Interfaces;
using Newtonsoft.Json;

namespace HotelBooker.Models;

/// <summary>
/// Represents a hotel
/// </summary>
public record Hotel
{
    /// <summary>
    /// The hotel's ID
    /// </summary>
    [JsonProperty("id")]
    [JsonRequired]
    public string Id { get; set; }


    /// <summary>
    /// The hotel's name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }


    /// <summary>
    /// The list of hotel's room types 
    /// </summary>
    [JsonProperty("roomTypes")]
    [JsonRequired]
    public List<RoomType> RoomTypes { get; set; }


    /// <summary>
    /// The list of hotel's rooms
    /// </summary>
    [JsonProperty("rooms")]
    [JsonRequired]
    public List<Room> Rooms { get; set; }
}

