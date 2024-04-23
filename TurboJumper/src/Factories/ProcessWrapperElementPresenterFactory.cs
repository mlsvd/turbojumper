using Microsoft.Extensions.Configuration;
using TurboJumper.Models;
using TurboJumper.Presenters;
using TurboJumper.Providers;

namespace TurboJumper.Factories;

public class ProcessWrapperElementPresenterFactory(IAppConfigurationProvider configuration)
{
    private IAppConfigurationProvider _configuration = configuration;
    
    public ProcessWrapperElementPresenter Create(ProcessWrapper processWrapper)
    {
        return new ProcessWrapperElementPresenter(processWrapper, this._configuration);
    }
}