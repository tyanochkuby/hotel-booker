using HotelBooker.Extensions;
using HotelBooker.Helpers;
using HotelBooker.Models;
using HotelBooker.Services;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        var config = CommandLineConfigurator.GetConfig(args);

        if (!File.Exists(config["Bookings"]) || !File.Exists(config["Hotels"]))
        {
            Console.WriteLine("Bad input, files not found");
            return;
        }
        
        string hotelsJson = await File.ReadAllTextAsync(config["Hotels"]);
        string bookingsJson = await File.ReadAllTextAsync(config["Bookings"]);

        List<Hotel> hotels = new List<Hotel>();
        List<Booking> bookings = new List<Booking>();

        try
        {
            hotels = JsonSerializer.Deserialize<List<Hotel>>(hotelsJson);
            bookings = JsonSerializer.Deserialize<List<Booking>>(bookingsJson);
        }
        catch (JsonException e)
        {
            Console.WriteLine(e.Message);
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
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }

            Console.WriteLine($"There's {availabilityService.GetCountAvailable(availability)} rooms of type {availability.RoomType} available in the {hotels.First(h => h.Id == availability.Code).Name}");
        }
    }
}
