using HotelBooker.Models;

namespace HotelBooker.Interfaces
{
    public interface IAvailabilityService
    {
        public int GetCountAvailable(Availability availability);
    }
}
