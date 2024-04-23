using System.Diagnostics;
using TurboJumper.Models;

namespace TurboJumper.Factories;

public class ProcessWrapperFactory
{
    public ProcessWrapper Create(Process process)
    {
        return new ProcessWrapper(process);
    }
}