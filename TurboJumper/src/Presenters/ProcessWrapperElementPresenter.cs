using TurboJumper.FormElement;
using TurboJumper.Listeners;
using TurboJumper.Models;
using TurboJumper.Providers;

namespace TurboJumper.Presenters;

public class ProcessWrapperElementPresenter(
    ProcessWrapper processWrapper, 
    IAppConfigurationProvider configuration,
    ProcessFormButtonListener processFormButtonListener
) : IProcessWrapperElementPresenter
{
    private ProcessWrapper _processWrapper = processWrapper;
    private IAppConfigurationProvider _configuration = configuration;
    private ProcessFormButtonListener _processFormButtonListener = processFormButtonListener;

    public Control ToFormElement(FormViewCoordinates formViewCoordinates)
    {
        var groupBoxWidth= this._configuration.ProvideIntValue("FormView:GroupBoxWidth", 150);
        var groupBoxHeight= this._configuration.ProvideIntValue("FormView:GroupBoxHeight", 80);

        var buttonWidth= this._configuration.ProvideIntValue("FormView:ButtonWidth", 130);
        var buttonHeight= this._configuration.ProvideIntValue("FormView:ButtonHeight", 60);
        
        GroupBox groupBox = new GroupBox();
        groupBox.Text = this._processWrapper.GetProcessName();
        groupBox.Location = new System.Drawing.Point(formViewCoordinates.X, formViewCoordinates.Y);
        groupBox.Size = new System.Drawing.Size(groupBoxWidth, groupBoxHeight);
        groupBox.FlatStyle = FlatStyle.System;

        ProcessButton button = new ProcessButton();
        button.Tag = processWrapper;
        button.Location = new System.Drawing.Point(5, 20);
        button.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
        button.Image = processWrapper.AppIcon;
        button.ImageAlign = ContentAlignment.MiddleLeft;
        button.TextImageRelation = TextImageRelation.ImageBeforeText;
        button.BottomText = processWrapper.KeyboardShortcut.DisplayName;
        

        button.Text = this._processWrapper.GetMainWindowTitle();
        int maxTextWidth = button.Width * 2 - 10 * 3;
        if (TextRenderer.MeasureText(button.Text, button.Font).Width > maxTextWidth)
        {
            button.Text = button.Text.Substring(0, Math.Min(button.Text.Length, maxTextWidth / TextRenderer.MeasureText("A", button.Font).Width)) + "...";
        }

        button.Click += this._processFormButtonListener.OnClick;
        
        /*Image settingsIcon = Image.FromFile("src\\Resources\\Images\\settings.png");
        Button keyShortcutButton = new Button();
        keyShortcutButton.Size = new Size(24, 24); 
        // keyShortcutButton.Text = processWrapper.KeyboardShortcut;
        keyShortcutButton.Location = new Point(groupBoxWidth - 24, groupBoxHeight - 24);
        keyShortcutButton.Font = new Font(button.Font.FontFamily, button.Font.Size - 2);
        // keyShortcutButton.Click += SmallButton_Click;
        keyShortcutButton.Image = settingsIcon;
        // keyShortcutButton.ImageAlign = ContentAlignment.TopLeft;

        groupBox.Controls.Add(keyShortcutButton);*/
        groupBox.Controls.Add(button);

        return groupBox;
    }
}
