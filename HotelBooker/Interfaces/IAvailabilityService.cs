using HotelBooker.Models;

namespace HotelBooker.Interfaces
{
    public interface IAvailabilityService
    {
        public Dictionary<DateTime, int> GetDetailedAvailability(Availability availability);
    }
}
