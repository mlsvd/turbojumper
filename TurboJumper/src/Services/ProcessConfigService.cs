using System.Text.Json;
using TurboJumper.Models;

namespace TurboJumper.Services;

public class ProcessConfigService
{
    private static readonly string ConfigDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "TurboJumper"
    );

    private static readonly string ConfigPath = Path.Combine(ConfigDir, "process-config.json");

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public AppProcessConfig Load()
    {
        if (!File.Exists(ConfigPath))
        {
            var defaultConfig = CreateDefault();
            Save(defaultConfig);
            return defaultConfig;
        }

        try
        {
            var json = File.ReadAllText(ConfigPath);
            return JsonSerializer.Deserialize<AppProcessConfig>(json, JsonOptions) ?? CreateDefault();
        }
        catch
        {
            return CreateDefault();
        }
    }

    public void Save(AppProcessConfig config)
    {
        Directory.CreateDirectory(ConfigDir);
        File.WriteAllText(ConfigPath, JsonSerializer.Serialize(config, JsonOptions));
    }

    private static AppProcessConfig CreateDefault() => new()
    {
        Processes =
        [
            new() { ProcessName = "chrome",   DisplayName = "Chrome",         HotKey = "D1", Order = 1,  Enabled = true },
            new() { ProcessName = "msedge",   DisplayName = "Edge",           HotKey = "D2", Order = 2,  Enabled = true },
            new() { ProcessName = "firefox",  DisplayName = "Firefox",        HotKey = "D3", Order = 3,  Enabled = true },
            new() { ProcessName = "Code",     DisplayName = "VS Code",        HotKey = "D4", Order = 4,  Enabled = true },
            new() { ProcessName = "slack",    DisplayName = "Slack",          HotKey = "D5", Order = 5,  Enabled = true },
            new() { ProcessName = "Teams",    DisplayName = "Teams",          HotKey = "D6", Order = 6,  Enabled = true },
            new() { ProcessName = "spotify",  DisplayName = "Spotify",        HotKey = "D7", Order = 7,  Enabled = true },
            new() { ProcessName = "devenv",   DisplayName = "Visual Studio",  HotKey = "q",  Order = 8,  Enabled = true },
            new() { ProcessName = "discord",  DisplayName = "Discord",        HotKey = "w",  Order = 9,  Enabled = true },
            new() { ProcessName = "explorer", DisplayName = "Explorer",       HotKey = "e",  Order = 10, Enabled = true },
            new() { ProcessName = "notepad",  DisplayName = "Notepad",        HotKey = "r",  Order = 11, Enabled = true },
        ]
    };
}
