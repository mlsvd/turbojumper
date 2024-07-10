using Microsoft.Extensions.Configuration;
using TurboJumper.Models;
using TurboJumper.Providers;

namespace TurboJumper.Factories;

public class FormViewCoordinatesFactory(IAppConfigurationProvider configuration)
{
    private IAppConfigurationProvider _configuration = configuration;

    public FormViewCoordinates Create(Form targetForm)
    {
        return new FormViewCoordinates(0, 0);
    }
}
