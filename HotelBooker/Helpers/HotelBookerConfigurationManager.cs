using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HotelBooker.Helpers;

/// <summary>
/// Configuration manager for Hotel Booker application.
/// </summary>
public static class HotelBookerConfigurationManager
{
    const string HOTELS = "Hotels";
    const string BOOKINGS = "Bookings";

    /// <summary>
    /// Gets the configuration from command line arguments.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    /// <returns>The configuration root.</returns>
    public static IConfigurationRoot GetConfig(string[] args)
    {
        var switchMappings = new Dictionary<string, string>()
        {
            { "--hotels", HOTELS },
            { "--bookings", BOOKINGS },
        };
        var builder = new ConfigurationBuilder();
        builder.AddCommandLine(args, switchMappings);

        return builder.Build();
    }

    /// <summary>
    /// Validates the configuration to ensure required values are present and files exist.
    /// </summary>
    /// <param name="config">The configuration to validate.</param>
    /// <returns>True if the configuration is valid; otherwise, false.</returns>
    public static bool ValidateConfig(IConfiguration config)
    {
        if (string.IsNullOrEmpty(config[HOTELS]) || string.IsNullOrEmpty(config[BOOKINGS]))
        {
            Console.WriteLine("Configuration values for Hotels or Bookings are missing.");
            return false;
        }

        if (!File.Exists(config[HOTELS]) || !File.Exists(config[BOOKINGS]))
        {
            Console.WriteLine("Bad input, files not found");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Tries to load JSON data from a file.
    /// </summary>
    /// <typeparam name="T">The type of data to load.</typeparam>
    /// <param name="filePath">The path to the JSON file.</param>
    /// <param name="data">The loaded data.</param>
    /// <returns>True if the data was loaded successfully; otherwise, false.</returns>
    public static bool TryLoadJson<T>(string filePath, out List<T> data)
    {
        data = new List<T>();
        try
        {
            string json = File.ReadAllText(filePath);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Error
            };
            data = JsonConvert.DeserializeObject<List<T>>(json, settings);
            return true;
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Error loading JSON from {filePath}: {e.Message}");
            return false;
        }
    }
}
