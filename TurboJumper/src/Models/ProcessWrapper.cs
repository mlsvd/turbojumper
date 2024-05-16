using System.Diagnostics;

namespace TurboJumper.Models;

public class ProcessWrapper(Process processOrigin)
{
    private Process _processOrigin = processOrigin;

    public Image? AppIcon { get; set; }

    public bool IsMainProcess()
    {
        return !string.IsNullOrEmpty(this._processOrigin.MainWindowTitle);
    }

    public string GetMainWindowTitle()
    {
        return this._processOrigin.MainWindowTitle;
    }
    
    public string GetProcessName()
    {
        return this._processOrigin.ProcessName;
    }
    
    public string? GetMainModuleFileName()
    {
        return this._processOrigin.MainModule?.FileName;
    }

    public IntPtr GetMainWindowHandle()
    {
        return this._processOrigin.MainWindowHandle;
    }
}
