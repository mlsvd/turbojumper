using Microsoft.Extensions.Configuration;

namespace TurboJumper.Providers;

public class AppConfigurationProvider(IConfiguration configuration): IAppConfigurationProvider
    {
    private IConfiguration _configuration = configuration;

    public string ProvideStringValue(string key, string defaultValue = "")
    {
        var value = this._configuration[key];
        if (string.IsNullOrEmpty(value))
        {
            return defaultValue;
        }

        return value;
    }
    
    public int ProvideIntValue(string key, int defaultValue = 0, int min = Int32.MinValue, int max = Int32.MaxValue)
    {
        var value = int.Parse(this._configuration[key]);
        if (value < min || value > max)
        {
            return defaultValue;
        }

        return value;
    }
}