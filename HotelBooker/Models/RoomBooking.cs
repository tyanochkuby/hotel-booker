using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooker.Models;

/// <summary>
/// Represents booked date ranges for a specific room type
/// </summary>
public record RoomBooking
{
    /// <summary>
    /// The room type
    /// </summary>
    public string RoomType { get; init; }


    /// <summary>
    /// The date ranges for which this room type is booked
    /// </summary>
    public List<DateRange> DateRanges { get; init; }
}
