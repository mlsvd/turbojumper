using System.Diagnostics;

namespace TurboJumper.Models;

public class ProcessWrapper(Process processOrigin)
{
    private Process _processOrigin = processOrigin;

    public static ProcessWrapper CreateFromProcess(Process process)
    {
        return new ProcessWrapper(process);
    }

    public bool IsMainProcess()
    {
        return !string.IsNullOrEmpty(this._processOrigin.MainWindowTitle);
    }

    public string GetMainWindowTitle()
    {
        return this._processOrigin.MainWindowTitle;
    }
}
