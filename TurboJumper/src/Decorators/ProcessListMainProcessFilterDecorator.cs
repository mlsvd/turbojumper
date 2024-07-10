using TurboJumper.Models;

namespace TurboJumper.Decorators;

public class ProcessListMainProcessFilterDecorator: IProcessListDecorator
{
    public List<ProcessWrapper> Decorate(List<ProcessWrapper> processWrappers)
    {
        List<ProcessWrapper> result = new List<ProcessWrapper>();
        foreach (ProcessWrapper processWrapper in processWrappers)
        {
            if (!processWrapper.IsMainProcess())
            {
                continue;
            }

            result.Add(processWrapper);
        }

        return result;
    }
}
