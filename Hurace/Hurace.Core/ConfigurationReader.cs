using Microsoft.Extensions.Configuration;

namespace Hurace.Core
{
    static class ConfigurationReader
    {
        private static IConfiguration configuration = null;

        public static IConfiguration GetConfiguration() =>
          configuration = configuration ?? new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        public static string GetConnectionString(Environment environment) =>
            GetConfiguration()
            .GetSection("ConnectionStrings")
            .GetSection(environment.ToString()).Value;
    }
}
