using Microsoft.Extensions.Configuration;

namespace Firebase.Test
{
    public static class SharedTestConfiguration
    {
        private static IConfigurationRoot configurationRoot;

        public static IConfigurationRoot Configuration
        {
            get
            {
                if (configurationRoot == null)
                {
                    configurationRoot = new ConfigurationBuilder()
                        .AddJsonFile("secrets.json")
                        .Build();
                }

                return configurationRoot;
            }
        }
    }
}
