using System;
using System.Collections.Generic;

namespace ConfigSettingsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawData = new Dictionary<string, string>()
            {
                {"key1", "value1"},
                {"key2", "1"},
                {"key3", "false"},
                {"key6", "05/27/2021"},

            };

            var configSettings = new ConfigSettings(rawData);
            string errorMessage;
            var setting1 = configSettings.GetValue<string>("key1", out errorMessage);
            var setting2 = configSettings.GetValue<int>("key2", out errorMessage);
            var setting3 = configSettings.GetValue<bool>("key3", out errorMessage);
            var setting6 = configSettings.GetValue<DateTime>("key6", out errorMessage);
            try
            {
                var setting4 = configSettings.GetValue<bool>("key4", out errorMessage);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
            try
            {
                var setting4 = configSettings.GetValue<bool>("key1", out errorMessage);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }

            var configSettings2 = new ConfigSettings(rawData, false);
            var setting5 = configSettings2.GetValue<bool>("key5", out errorMessage);
            Console.WriteLine(errorMessage);
            var setting7 = configSettings2.GetValue<bool>("key1", out errorMessage);
            Console.WriteLine(errorMessage);
        }
    }
}
