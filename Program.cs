using System;
using DbConnect.Services;

namespace DbConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentService parsing = new ArgumentService();
            int parsingresult = parsing.ParsingArguments(args);

            if (parsingresult == 1)
            {
                Console.WriteLine("\nUsage: DbConnect.exe <-ping|-sql> <database>\n");
                Environment.Exit(0);
            }
            
            string commandname = args[0];
            string databasename = args[1];

            // Build the configuration settings to be used in this application.
            ConfigurationService service = new ConfigurationService(databasename);
            var configuration = service.Configuration;

            DatabaseService database = new DatabaseService();

            // Get connection string from configuration source. 
            string connectionstring = database.ConnectionString(configuration, databasename);

            switch (commandname)
            {
                case "-ping":

                    // Get database status using the connection string from configuration source.
                    string statusmessage = database.ConnectionStatus(connectionstring, databasename);
                    Console.WriteLine(statusmessage);

                break;

                case "-sql":

                    // Run SQL command using the connection string from configuration source. 
                    string commandstring = database.CommandString(configuration, databasename);
                    database.Command(connectionstring, commandstring);

                break;
            }
        }
    }
}
