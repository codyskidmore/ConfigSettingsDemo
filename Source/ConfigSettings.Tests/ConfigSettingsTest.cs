using System;
using System.Collections.Generic;
using Xunit;

namespace ConfigSettings.Tests
{
    public class ConfigSettingsTest
    {
        private readonly ConfigSettingsDemo.ConfigSettings _configSettings = new ConfigSettingsDemo.ConfigSettings(new Dictionary<string, string>()
        {
            {"key1", "value1"},
            {"key2", "1"},
            {"key3", "false"},
            {"key4", "05/26/2021"},
        });
        private readonly ConfigSettingsDemo.ConfigSettings _configSettingsError = new ConfigSettingsDemo.ConfigSettings(new Dictionary<string, string>()
        {
            {"key1", "value1"},
            {"key2", "1"},
            {"key3", "false"},
            {"key4", "05/26/2021"},
        }, false);

        [Fact]
        public void TestStringValue()
        {
            var setting = _configSettings.GetValue<string>("key1", out _);
            Assert.Matches("value1", setting);
        }

        [Fact]
        public void TestIntValue()
        {
            var setting = _configSettings.GetValue<int>("key2", out _);
            Assert.Equal(1, setting);
        }

        [Fact]
        public void TestBoolValue()
        {
            var setting = _configSettings.GetValue<bool>("key3", out _);
            Assert.False(setting);
        }

        [Fact]
        public void TestDateValue()
        {
            var setting = _configSettings.GetValue<DateTime>("key4", out _);
            Assert.Equal(new DateTime(2021, 05, 26), setting);
        }

        [Fact]
        public void TestErrorMessageIsEmpty()
        {
            _configSettings.GetValue<string>("key1", out string message);
            Assert.Empty(message);
        }

        [Fact]
        public void TestThrowsCannotConvertArgumentException()
        {
            try
            {
                var setting4 = _configSettings.GetValue<bool>("key1", out _);
                Assert.True(false);
            }
            catch (ArgumentException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void TestReturnsCannotConvertErrorMessage()
        {
            _configSettingsError.GetValue<bool>("key1", out string errorMessage);
            Assert.Matches("Cannot convert 'value1' for key 'key1' to type 'System.Boolean'.", errorMessage);
        }

        [Fact]
        public void TestThrowsNotFoundArgumentException()
        {
            try
            {
                var setting4 = _configSettings.GetValue<bool>("key5", out _);
                Assert.True(false);
            }
            catch (ArgumentException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void TestReturnsNotFoundErrorMessage()
        {
            _configSettingsError.GetValue<bool>("key5", out string errorMessage);
            Assert.Matches("Key 'key5' does not exist in settings.", errorMessage);
        }
    }
}
