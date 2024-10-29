namespace HotelBooker.Models
{
    public class Availability
    {
        private Availability()
        {
        }

        public Availability(string code, string roomType, DateRange dateRange)
        {
            Code = code;
            RoomType = roomType;
            DateRange = dateRange;
        }

        public string Code { get; set; }
        public string RoomType { get; set; }
        public DateRange DateRange { get; set; }
    }
}