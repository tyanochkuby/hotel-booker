using HotelBooker.Helpers;
using HotelBooker.Interfaces;
using HotelBooker.Models;
using HotelBooker.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

public class Program
{
    public static void Main(string[] args)
    {
        var config = HotelBookerConfigurationManager.GetConfig(args);

        if (!HotelBookerConfigurationManager.ValidateConfig(config))
        {
            return;
        }

        List<Hotel> hotels;
        List<Booking> bookings;

        if (!HotelBookerConfigurationManager.TryLoadJson(config["Hotels"], out hotels) ||
            !HotelBookerConfigurationManager.TryLoadJson(config["Bookings"], out bookings))
        {
            return;
        }

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IAvailabilityService>(sp => new AvailabilityService(hotels, bookings))
            .AddSingleton<IAvailabilityParser, AvailabilityParser>()
            .AddSingleton<IDataValidator, DataValidator>()
            .BuildServiceProvider();

        var dataValidator = serviceProvider.GetRequiredService<IDataValidator>();
        var availabilityService = serviceProvider.GetRequiredService<IAvailabilityService>();
        var availabilityParser = serviceProvider.GetRequiredService<IAvailabilityParser>();

        try
        {
            dataValidator.ValidateHotelAndBookings(hotels, bookings);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        Console.WriteLine("Jsons were loaded successfully");

        string input;
        while ((input = Console.ReadLine()) != null && input != "")
        {
            Availability availability;
            try
            {
                availability = availabilityParser.Parse(input);
                dataValidator.ValidateAvailability(availability);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                continue;
            }

            try
            {
                var detailedAvailability = availabilityService.GetDetailedAvailability(availability);

                Console.WriteLine($"There are {detailedAvailability.Values.Min()} room(s) of type {availability.RoomType} in the {hotels.First(h => h.Id == availability.HotelId).Name} hotel available for the entire period from {availability.DateRange.Start.ToShortDateString()} to {availability.DateRange.End.ToShortDateString()}.");
                if (detailedAvailability.Count == 1)
                {
                    continue;
                }
                Console.WriteLine($"Detailed availability for {availability.RoomType} in {hotels.First(h => h.Id == availability.HotelId).Name}:");
                foreach (var entry in detailedAvailability)
                {
                    Console.WriteLine($"\t{entry.Key.ToShortDateString()} - {entry.Key.AddDays(1).ToShortDateString()}: {entry.Value} room(s) available");
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
