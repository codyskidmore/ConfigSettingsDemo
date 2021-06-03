using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ConfigSettingsDemo
{
    public interface IConfigSettings : IDictionary
    {
        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <param name="errorMessage">The error message returned if the setting cannot be converted.</param>
        /// <returns>The converted value.</returns>
        T GetValue<T>(string key, out string errorMessage);

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <exception>Throws ArgumentException</exception>
        /// <returns>The converted value.</returns>
        T GetValue<T>(string key);

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <param name="defaultValue">A default value to return if the setting does not exist.</param>
        /// <param name="errorMessage">The error message returned if the setting cannot be converted.</param>
        /// <returns>The converted value.</returns>
        T GetValue<T>(string key, T defaultValue, out string errorMessage);

        /// <summary>
        /// Extracts the value with the specified key and converts it to type T.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="key">The key of the configuration section's value to convert.</param>
        /// <param name="defaultValue">A default value to return if the setting does not exist.</param>
        /// <exception>Throws ArgumentException</exception>
        /// <returns>The converted value.</returns>
        T GetValue<T>(string key, T defaultValue);
    }
}