using TurboJumper.Factories;
using TurboJumper.Models;
using TurboJumper.Providers;

namespace TurboJumper.Presenters;

public class ProcessListPresenter(
    FormViewCoordinatesFactory viewCoordinatesFactory, 
    ProcessWrapperElementPresenterFactory elementPresenterFactory,
    IAppConfigurationProvider configration
): IProcessListPresenter
{
    private FormViewCoordinatesFactory _viewCoordinates = viewCoordinatesFactory;
    private ProcessWrapperElementPresenterFactory _elementPresenterFactory = elementPresenterFactory;
    private IAppConfigurationProvider _configuration = configration;
    
    public Form Present(Form targetForm, List<ProcessWrapper> processWrappers)
    {
        var formViewCoordinates = this._viewCoordinates.Create(targetForm);

        foreach (ProcessWrapper processWrapper in processWrappers)
        {
            var elementPresenter = this._elementPresenterFactory.Create(processWrapper);
            targetForm.Controls.Add(elementPresenter.ToFormElement(formViewCoordinates));

            formViewCoordinates.X += this._configuration.ProvideIntValue("FormView:GroupBoxWidth", 150);
            formViewCoordinates.X += this._configuration.ProvideIntValue("FormView:GroupBoxOffsetX", 10);
            if (formViewCoordinates.X >= this._configuration.ProvideIntValue("FormView:FormWidth", 800))
            {
                formViewCoordinates.X = 0;
                formViewCoordinates.Y += this._configuration.ProvideIntValue("FormView:GroupBoxHeight", 80);
                formViewCoordinates.Y += this._configuration.ProvideIntValue("FormView:GroupBoxOffsetY", 10);
            }
        }
        
        return targetForm;
    }
}
