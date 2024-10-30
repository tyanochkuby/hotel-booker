using Microsoft.Extensions.Configuration;

namespace HotelBooker.Helpers
{
    public static class CommandLineConfigurator
    {
        public static IConfigurationRoot GetConfig(string[] args)
        {
            var switchMappings = new Dictionary<string, string>()
           {
               { "--hotels", "Hotels" },
               { "--bookings", "Bookings" },
           };
            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args, switchMappings);

            return builder.Build();
        }
    }
}
