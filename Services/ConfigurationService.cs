using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using DbConnect.Services;

namespace DbConnect.Services
{
    public class ConfigurationService
    {
        public IConfigurationRoot Configuration { get; set; }

        public ConfigurationService(string databasename)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("DbConfig.json", optional: true, reloadOnChange: true);

                Configuration = builder.Build();

                if (!Configuration.GetSection(databasename).Exists())
                {
                    Console.WriteLine("\nValue Error: Please provide a valid database name.");
                    Console.WriteLine("The database was not found in the configuration file.");
                    
                    ArgumentService parsing = new ArgumentService();
                    parsing.ParsingSingleArgumentString(databasename);
                    Environment.Exit(0);
                }
        }
    }
}