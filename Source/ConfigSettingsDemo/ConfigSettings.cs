using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ConfigSettingsDemo
{
    public class ConfigSettings : Dictionary<string, string>, IConfigSettings
    {
        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <param name="errorMessage">The error message returned if the setting cannot be converted.</param>
        /// <returns>The converted value.</returns>
        public T GetValue<T>(string key, out string errorMessage)
        {
            T result = default;

            if (!TryGetValue(key, out string value))
            {
                errorMessage = $"Key '{key}' does not exist in settings.";
                return result;
            }

            return ConvertValue<T>(key, value, out errorMessage);
        }

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <param name="defaultValue">A default value to return if the setting does not exist.</param>
        /// <param name="errorMessage">The error message returned if the setting cannot be converted.</param>
        /// <returns>The converted value.</returns>
        public T GetValue<T>(string key, T defaultValue, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!TryGetValue(key, out string value))
            {
                return defaultValue;
            }

            return ConvertValue<T>(key, value, out errorMessage);

        }

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <exception>Throws ArgumentException</exception>
        /// <returns>The converted value.</returns>
        public T GetValue<T>(string key)
        {
            Type type = typeof(T);

            if (!TryGetValue(key, out string value))
            {
                throw new ArgumentException($"Key '{key}' does not exist in settings.");
            }

            return ConvertValue<T>(key, type, value);
        }

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <param name="defaultValue">A default value to return if the setting does not exist.</param>
        /// <exception>Throws ArgumentException</exception>
        /// <returns>The converted value.</returns>
        public T GetValue<T>(string key, T defaultValue)
        {
            Type type = typeof(T);

            if (!TryGetValue(key, out string value))
            {
                return defaultValue;
            }

            return ConvertValue<T>(key, type, value);
        }

        private T ConvertValue<T>(string key, string value, out string errorMessage)
        {
            Type type = typeof(T);
            T result = default;

            var converter = TypeDescriptor.GetConverter(type);
            if (!converter.CanConvertFrom(typeof(string)))
            {
                errorMessage = $"Cannot convert from string to type {type}.";
                return result;
            }

            try
            {
                errorMessage = string.Empty;
                return (T)converter.ConvertFromInvariantString(value);
            }
            catch (Exception)
            {
                errorMessage = $"Error while converting from string to type '{type}' for key '{key}'.";
                return result;
            }
        }

        private T ConvertValue<T>(string key, Type type, string value)
        {
            var converter = TypeDescriptor.GetConverter(type);
            if (!converter.CanConvertFrom(typeof(string)))
            {
                throw new ArgumentException($"Cannot convert from string to type {type}.");
            }

            try
            {
                return (T)converter.ConvertFromInvariantString(value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while converting from string to type '{type}' for key '{key}'.", ex);
            }
        }
    }
}