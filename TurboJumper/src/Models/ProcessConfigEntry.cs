namespace TurboJumper.Models;

public class ProcessConfigEntry
{
    public string ProcessName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string HotKey { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool Enabled { get; set; } = true;
}
