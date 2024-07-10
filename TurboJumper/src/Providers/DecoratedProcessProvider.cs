using TurboJumper.Decorators;
using TurboJumper.Models;

namespace TurboJumper.Providers;

public class DecoratedProcessProvider: IProcessProvider
{
    private ProcessProvider _processProvider;
    private List<IProcessListDecorator> _decorators = [];
    
    public DecoratedProcessProvider(
        ProcessProvider processProvider,
        ProcessListMainProcessFilterDecorator mainProcessFilterDecorator,
        ProcessListIconDecorator iconDecorator,
        ProcessListKeyCombinationDecorator keyCombinationDecorator
    )
    {
        this._processProvider = processProvider;
        this._decorators.Add(mainProcessFilterDecorator);
        this._decorators.Add(iconDecorator);
        this._decorators.Add(keyCombinationDecorator);
    }
    
    public List<ProcessWrapper> Provide()
    {
        var processList = this._processProvider.Provide();

        foreach (IProcessListDecorator decorator in this._decorators)
        {
            processList = decorator.Decorate(processList);
        }
        
        return processList;
    }
}
