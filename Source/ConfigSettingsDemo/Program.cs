using System;

namespace ConfigSettingsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var configSettings = new ConfigSettings {
                {"key1", "value1"},
                {"key2", "1"},
                {"key3", "false"},
                {"key4", "05/27/2021"}
            };
            string errorMessage;

            var setting1 = configSettings.GetValue<string>("key1", out errorMessage);
            var setting2 = configSettings.GetValue<int>("key2", out errorMessage);
            var setting3 = configSettings.GetValue<bool>("key3", out errorMessage);
            var setting6 = configSettings.GetValue<DateTime>("key6", out errorMessage);
            var setting7 = configSettings["key4"];
            setting3 = configSettings.GetValue<bool>("key3");
            setting2 = configSettings.GetValue("key2", 10);

            // Conversion Error
            try
            {
                var setting4 = configSettings.GetValue<bool>("key4");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }

            // Key not found
            var setting5 = configSettings.GetValue<bool>("key5", out errorMessage);
        }
    }
}
