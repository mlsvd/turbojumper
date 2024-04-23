using Microsoft.Extensions.Configuration;
using TurboJumper.Models;
using TurboJumper.Providers;

namespace TurboJumper.Presenters;

public class ProcessWrapperElementPresenter(ProcessWrapper processWrapper, IAppConfigurationProvider configuration) : IProcessWrapperElementPresenter
{
    private ProcessWrapper _processWrapper = processWrapper;
    private IAppConfigurationProvider _configuration = configuration;

    public Control ToFormElement(FormViewCoordinates formViewCoordinates)
    {
        Console.WriteLine(this._processWrapper.GetMainWindowTitle());
        // Console.WriteLine(this._configuration["FormView:GroupBoxWidth1"]);
        // throw new Exception();
        var groupBoxWidth= this._configuration.ProvideIntValue("FormView:GroupBoxWidth", 150);
        var groupBoxHeight= this._configuration.ProvideIntValue("FormView:GroupBoxHeight", 80);

        var buttonWidth= this._configuration.ProvideIntValue("FormView:ButtonWidth", 130);
        var buttonHeight= this._configuration.ProvideIntValue("FormView:ButtonHeight", 60);
        
        GroupBox groupBox = new GroupBox();
        groupBox.Text = this._processWrapper.GetProcessName();
        groupBox.Location = new System.Drawing.Point(formViewCoordinates.X, formViewCoordinates.Y);
        groupBox.Size = new System.Drawing.Size(groupBoxWidth, groupBoxHeight);
        groupBox.FlatStyle = FlatStyle.System;

        Button button = new Button();
        button.Text = this._processWrapper.GetMainWindowTitle();
        button.Location = new System.Drawing.Point(5, 25);
        button.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
        groupBox.Controls.Add(button);

        return groupBox;
    }
}
