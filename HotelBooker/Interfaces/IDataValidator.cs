using HotelBooker.Models;

namespace HotelBooker.Interfaces
{
    /// <summary>
    /// Interface for the hotel data validator.
    /// </summary>
    public interface IDataValidator
    {
        /// <summary>
        /// Validates the availability object.
        /// </summary>
        /// <param name="availability">The availability object to validate.</param>
        /// <exception cref="ArgumentException">Thrown when the availability object is invalid.</exception>
        void ValidateAvailability(AvailabilityQuery availability);

        /// <summary>
        /// Validates the list of hotels and bookings.
        /// </summary>
        /// <param name="hotels">The list of hotels to validate.</param>
        /// <param name="bookings">The list of bookings to validate.</param>
        /// <exception cref="ArgumentException">Thrown when the hotels or bookings list is invalid.</exception>
        void ValidateHotelAndBookings(List<Hotel> hotels, List<Booking> bookings);
    }
}