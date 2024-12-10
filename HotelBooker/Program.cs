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
            .AddSingleton<ILogger, ConsoleLogger>()
            .BuildServiceProvider();

        var dataValidator = serviceProvider.GetRequiredService<IDataValidator>();
        var availabilityService = serviceProvider.GetRequiredService<IAvailabilityService>();
        var availabilityParser = serviceProvider.GetRequiredService<IAvailabilityParser>();
        var logger = serviceProvider.GetRequiredService<ILogger>();

        try
        {
            dataValidator.ValidateHotelAndBookings(hotels, bookings);
        }
        catch (ArgumentException e)
        {
            logger.LogError(e.Message);
            return;
        }

        Console.WriteLine("Jsons were loaded successfully");

        string input;
        while ((input = Console.ReadLine()) != null && input != "")
        {
            AvailabilityQuery availabilityQuery;
            try
            {
                availabilityQuery = availabilityParser.Parse(input);
                dataValidator.ValidateAvailability(availabilityQuery);
            }
            catch (FormatException e)
            {
                logger.LogError(e.Message);
                continue;
            }
            catch (ArgumentException e)
            {
                logger.LogError(e.Message);
                continue;
            }

            try
            {
                var availability = availabilityService.GetTotalAvailability(availabilityQuery);
                logger.Log(availability.ToString());
            }
            catch (ArgumentException e)
            {
                logger.LogError(e.Message);
            }
        }
    }
}
