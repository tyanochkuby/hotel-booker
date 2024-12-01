namespace HotelBooker.Models
{
    public record DateRange
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }

        private DateRange()
        {
        }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }

}