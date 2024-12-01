namespace HotelBooker.Models
{
    public record Availability
    {
        private Availability()
        {
        }

        public Availability(string code, string roomType, DateRange dateRange)
        {
            HotelId = code;
            RoomType = roomType;
            DateRange = dateRange;
        }

        public string HotelId { get; init; }
        public string RoomType { get; init; }
        public DateRange DateRange { get; init; }
    }
}