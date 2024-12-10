using HotelBooker.Models;

namespace HotelBooker.Interfaces
{
    /// <summary>
    /// Interface for the availability service
    /// </summary>
    public interface IAvailabilityService
    {
        /// <summary>
        /// Get total availability of a room type in a hotel
        /// </summary>
        /// <param name="availability">Availability object</param>
        /// <returns>The amount of rooms free for a specified period</returns>
        public int GetTotalAvailability(AvailabilityQuery availability);

    }
}
