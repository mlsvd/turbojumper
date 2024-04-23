using System.Diagnostics;
using TurboJumper.Models;

namespace TurboJumper.Providers;

public class ProcessProvider: IProcessProvider
{
    public List<ProcessWrapper> Provide()
    {
        var processes = Process.GetProcesses();

        return processes.Select(ProcessWrapper.CreateFromProcess).ToList();
    }
}
