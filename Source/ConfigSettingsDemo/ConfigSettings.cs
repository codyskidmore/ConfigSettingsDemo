using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ConfigSettingsDemo
{
    /// <summary>
    /// A Sample for getting readonly settings
    /// and casting them to an expected type.
    /// </summary>
    public class ConfigSettings
    {
        private readonly IDictionary<string, string> _settings;
        private delegate void ExceptionHandler(string errorMessage);
        private readonly ExceptionHandler _exceptionHandler;

        private void ThrowArgumentException(string errorMessage)
        {
            throw new ArgumentException(errorMessage);
        }

        private void NoOpException(string errorMessage)
        {
            // Do nothing when throwing exceptions is turned off.
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings">A dictionary containing settings</param>
        /// <param name="throwException">A flag indicating to throw ArgumentException if the setting cannot be converted. Defaults to TRUE.</param>
        public ConfigSettings(IDictionary<string, string> settings, bool throwException = true)
        {
            _settings = settings;
            _exceptionHandler = throwException ? ThrowArgumentException : NoOpException;
        }

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <param name="errorMessage">The error message returned if the setting cannot be converted.</param>
        /// <returns>The converted value.</returns>
        public T GetValue<T>(string key, out string errorMessage)
        {
            Type type = typeof(T);
            T result = default;

            bool settingFound = _settings.TryGetValue(key, out string value);

            if (!settingFound)
            {
                errorMessage = $"Key '{key}' does not exist in settings.";
                _exceptionHandler(errorMessage);
                return result;
            }

            var converter = TypeDescriptor.GetConverter(type);
            if (!converter.CanConvertFrom(typeof(string)))
            {
                errorMessage = $"Cannot convert from string to type {type}.";
                _exceptionHandler(errorMessage);
            }

            try
            {
                errorMessage = string.Empty;
                return (T)converter.ConvertFromInvariantString(value);
            }
            catch (Exception)
            {
                errorMessage = $"Cannot convert '{value}' for key '{key}' to type '{type}'.";
                _exceptionHandler(errorMessage);
                return result;
            }
        }
    }
}