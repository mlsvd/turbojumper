using TurboJumper.Factories;
using TurboJumper.Models;
using TurboJumper.Providers;

namespace TurboJumper.Presenters;

public class ProcessListPresenter(
    ProcessWrapperElementPresenterFactory elementPresenterFactory,
    IAppConfigurationProvider configration
): IProcessListPresenter
{
    private ProcessWrapperElementPresenterFactory _elementPresenterFactory = elementPresenterFactory;
    private IAppConfigurationProvider _configuration = configration;
    
    public void Present(Control container, List<ProcessWrapper> processWrappers)
    {
        container.Controls.Clear();

        var formViewCoordinates = new FormViewCoordinates(0, 0);
        var groupBoxWidth = this._configuration.ProvideIntValue("FormView:GroupBoxWidth", 150);
        var groupBoxOffsetX = this._configuration.ProvideIntValue("FormView:GroupBoxOffsetX", 10);
        var groupBoxHeight = this._configuration.ProvideIntValue("FormView:GroupBoxHeight", 80);
        var groupBoxOffsetY = this._configuration.ProvideIntValue("FormView:GroupBoxOffsetY", 10);
        var containerWidth = this._configuration.ProvideIntValue("FormView:FormWidth", 800);

        foreach (ProcessWrapper processWrapper in processWrappers)
        {
            var elementPresenter = this._elementPresenterFactory.Create(processWrapper);
            container.Controls.Add(elementPresenter.ToFormElement(formViewCoordinates));

            formViewCoordinates.X += groupBoxWidth + groupBoxOffsetX;
            if (formViewCoordinates.X + groupBoxWidth > containerWidth)
            {
                formViewCoordinates.X = 0;
                formViewCoordinates.Y += groupBoxHeight + groupBoxOffsetY;
            }
        }
    }
}
