namespace HotelBooker.Models;

/// <summary>
/// Represents a date range.
/// </summary>
public record DateRange
{
    /// <summary>
    /// The start date of the range.
    /// </summary>
    public DateTime Start { get; init; }


    /// <summary>
    /// The end date of the range.
    /// </summary>
    public DateTime End { get; init; }


    /// <summary>
    /// The private constructor
    /// </summary>
    private DateRange()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="DateRange"/> class.
    /// </summary>
    /// <param name="start">Start date</param>
    /// <param name="end">End date</param>
    public DateRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }
}