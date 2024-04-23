using System.Diagnostics;
using TurboJumper.Factories;
using TurboJumper.Models;

namespace TurboJumper.Providers;

public class ProcessProvider(ProcessWrapperFactory processWrapperFactory): IProcessProvider
{
    private ProcessWrapperFactory _processWrapperFactory = processWrapperFactory;
    
    public List<ProcessWrapper> Provide()
    {
        var processes = Process.GetProcesses();

        return processes.Select(this._processWrapperFactory.Create).ToList();
    }
}
