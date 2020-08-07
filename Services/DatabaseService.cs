using System;
using Microsoft.Extensions.Configuration;
using DbConnect.Configurations;
using MySqlConnector;

namespace DbConnect.Services
{
    public class DatabaseService
    {
        public string Message { get; set; }

        public string ConnectionString(IConfigurationRoot configuration, string databasename)
        {
            // Bind the content of default configuration to an instance of MySqlSetting.
            // Return the connection string found in the configuration source.

            DatabaseConfiguration setting = configuration.GetSection(databasename).Get<DatabaseConfiguration>();
            return setting.ConnectionString;
        }

        public string CommandString(IConfigurationRoot configuration, string databasename)
        {
            // Bind the content of default configuration to an instance of MySqlSetting.
            // Return the connection string found in the configuration source.

            DatabaseConfiguration setting = configuration.GetSection(databasename).Get<DatabaseConfiguration>();
            return setting.SqlCommand;
        }

        public string ConnectionStatus(string connectionstring, string databasename)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionstring))
                {
                    connection.Open();

                    bool connected = connection.Ping();

                    if (connected)
                    {
                        Message = $"{databasename}: Database is online!";
                    }
                    else 
                    {
                        Message = $"{databasename}: Database is offline!";
                    }

                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Message = $"MySql Error {ex.Number}: Cannot connect to the server. Contact your administrator.";
                        break;
                    case 1042:
                        Message = $"MySql Error {ex.Number}: Unable to connect to any of the specified MySQL hosts. Contact your administrator.";
                        break;
                    case 1045:
                        Message = $"MySql Error {ex.Number}: Invalid username/password, please try again.";
                        break;
                    default:
                        Message = $"MySql Error {ex.Number}: Unknown error when trying to connect to the server. Contact your administrator.";
                        break;
                }
            }
            return Message;
        }

        public void Command(string connectionstring, string commandstring)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionstring))
                {
                    connection.Open();

                    using (var command = new MySqlCommand($"{commandstring};", connection))
                    using (var reader = command.ExecuteReader())
                    while (reader.Read())

                    Console.WriteLine(reader.GetString(0));

                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine($"MySql Error {ex.Number}: Cannot connect to the server. Contact your administrator.");
                        break;
                    case 1042:
                        Console.WriteLine($"MySql Error {ex.Number}: Unable to connect to any of the specified MySQL hosts. Contact your administrator.");
                        break;
                    case 1045:
                        Console.WriteLine($"MySql Error {ex.Number}: Invalid username/password, please try again.");
                        break;
                    default:
                        Console.WriteLine($"MySql Error {ex.Number}: Unknown error when trying to connect to the server. Contact your administrator.");
                        break;
                }
            }
        }
    }
}