using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HotelBooker.Helpers
{
    public static class ConfigurationManager
    {
        const string HOTELS = "Hotels";
        const string BOOKINGS = "Bookings";
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
}
