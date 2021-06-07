# ConfigSettingsDemo
Sample source code showing how to create a dictionary for working with app settings from a database or other source. It returns the setting as an expected type. It can return a default value when no matching setting exists. It can also return either an error message or throw ArgumentException. 

The implementation follows the Single Responsibility principle. You need a provider and/or factory that returns an instance of IDictionary<string,string> containing the settings. The provider can be any source.
