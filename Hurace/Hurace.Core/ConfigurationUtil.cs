using Microsoft.Extensions.Configuration;

namespace Hurace.Core
{
    public static class ConfigurationUtil
    {
        private static IConfiguration configuration = null;

        public static IConfiguration GetConfiguration() =>
          configuration = configuration ?? new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        public static string GetConnectionString(string configName) =>
            GetConfiguration()
            .GetSection("ConnectionStrings")
            .GetSection(configName).Value;
    }
}
