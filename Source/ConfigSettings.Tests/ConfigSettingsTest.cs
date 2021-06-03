using System;
using System.Collections.Generic;
using Xunit;

namespace ConfigSettings.Tests
{
    public class ConfigSettingsTest
    {
        private readonly ConfigSettingsDemo.IConfigSettings _configSettings = new ConfigSettingsDemo.ConfigSettings
        {
            {"key1", "value1"},
            {"key2", "1"},
            {"key3", "false"},
            {"key4", "05/26/2021"},
        };

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
                var setting4 = _configSettings.GetValue<bool>("key1");
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
            _configSettings.GetValue<bool>("key1", out string errorMessage);
            Assert.Matches("Error while converting from string to type 'System.Boolean' for key 'key1'.", errorMessage);
        }

        [Fact]
        public void TestThrowsNotFoundArgumentException()
        {
            try
            {
                var setting4 = _configSettings.GetValue<bool>("key5");
                Assert.True(false);
            }
            catch (ArgumentException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void TestShouldReturnDefaultValueInsteadOfException()
        {
            try
            {
                var setting4 = _configSettings.GetValue("key5", 10);
                Assert.True(true);
            }
            catch (ArgumentException)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void TestShouldReturnDefaultValueInsteadOfErrorMessage()
        {
            try
            {
                var setting4 = _configSettings.GetValue("key5", 10, out _);
                Assert.True(true);
            }
            catch (ArgumentException)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void TestReturnsNotFoundErrorMessage()
        {
            _configSettings.GetValue<bool>("key5", out string errorMessage);
            Assert.Matches("Key 'key5' does not exist in settings.", errorMessage);
        }
    }
}
