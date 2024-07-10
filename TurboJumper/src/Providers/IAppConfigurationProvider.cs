namespace TurboJumper.Providers;

public interface IAppConfigurationProvider
{
    public string ProvideStringValue(string key, string defaultValue = "");
    public int ProvideIntValue(string key, int defaultValue = 0, int min = Int32.MinValue, int max = Int32.MaxValue);
}
