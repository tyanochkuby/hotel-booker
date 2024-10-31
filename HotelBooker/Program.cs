using HotelBooker.Extensions;
using HotelBooker.Helpers;
using HotelBooker.Models;
using HotelBooker.Services;
using System.Text.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        var config = ConfigurationManager.GetConfig(args);

        if (!ConfigurationManager.ValidateConfig(config))
        {
            return;
        }

        List<Hotel> hotels;
        List<Booking> bookings;

        if (!ConfigurationManager.TryLoadJson(config["Hotels"], out hotels) ||
            !ConfigurationManager.TryLoadJson(config["Bookings"], out bookings))
        {
            return;
        }

        if (!DataValidator.ValidateData(hotels, bookings))
        {
            return;
        }

        Console.WriteLine("Jsons were loaded successfully");


        AvailabilityService availabilityService = new AvailabilityService(hotels, bookings);

        string input;
        while ((input = Console.ReadLine()) != null && input != "")
        {
            Availability availability;
            try
            {
                availability = input.ToAvailability();
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }

            try 
            { 
                Console.WriteLine($"There's {availabilityService.GetCountAvailable(availability)} rooms of type {availability.RoomType} available in the {hotels.First(h => h.Id == availability.Code).Name}");
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
