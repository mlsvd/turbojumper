using TurboJumper.Decorators;
using TurboJumper.Models;

namespace TurboJumper.Providers;

public class DecoratedProcessProvider: IProcessProvider
{
    private ProcessProvider _processProvider;
    private List<IProcessListDecorator> _decorators = [];
    
    public DecoratedProcessProvider(
        ProcessProvider processProvider,
        ProcessListMainProcessFilterDecorator processListMainProcessFilterDecorator)
    {
        this._processProvider = processProvider;
        this._decorators
            .Add(processListMainProcessFilterDecorator)
            ;
    }
    
    public List<ProcessWrapper> Provide()
    {
        var processes = this._processProvider.Provide();

        return processes;
    }
}
