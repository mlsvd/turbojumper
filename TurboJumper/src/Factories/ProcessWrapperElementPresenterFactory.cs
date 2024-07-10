using Microsoft.Extensions.Configuration;
using TurboJumper.Listeners;
using TurboJumper.Models;
using TurboJumper.Presenters;
using TurboJumper.Providers;

namespace TurboJumper.Factories;

public class ProcessWrapperElementPresenterFactory(
    IAppConfigurationProvider configuration,
    ProcessFormButtonListener processFormButtonListener
)
{
    private IAppConfigurationProvider _configuration = configuration;
    private ProcessFormButtonListener _processFormButtonListener = processFormButtonListener;
    
    public ProcessWrapperElementPresenter Create(ProcessWrapper processWrapper)
    {
        return new ProcessWrapperElementPresenter(processWrapper, this._configuration, this._processFormButtonListener);
    }
}