namespace HotelBooker.Models
{
    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        private DateRange()
        {
        }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public bool Overlaps(DateRange other)
        {
            return Start < other.End && End > other.Start;
        }
    }

}