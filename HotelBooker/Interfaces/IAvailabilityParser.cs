using HotelBooker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooker.Interfaces
{
    /// <summary>
    /// An interface for parsing availability
    /// </summary>
    public interface IAvailabilityParser
    {
        /// <summary>
        /// Parses a string input into an Availability object
        /// </summary>
        /// <param name="input">A string of Availability(hotelId, yyyyMMdd, roomType) or Availability(hotelId, yyyyMMdd-yyyyMMdd, roomType) format</param>
        /// <returns>Availability object</returns>
        /// <exception cref="FormatException"></exception>
        AvailabilityQuery Parse(string input);
    }
}
