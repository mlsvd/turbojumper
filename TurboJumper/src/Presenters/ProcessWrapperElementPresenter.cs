using TurboJumper.Models;

namespace TurboJumper.Presenters;

public class ProcessWrapperElementPresenter(ProcessWrapper processWrapper) : IProcessWrapperElementPresenter
{
    private ProcessWrapper _processWrapper = processWrapper;

    public static ProcessWrapperElementPresenter CreateFromProcessWrapper(ProcessWrapper processWrapper)
    {
        return new ProcessWrapperElementPresenter(processWrapper);
    }

    public Control toFormElement(FormViewCoordinates formViewCoordinates)
    {
        GroupBox groupBox = new GroupBox();
        groupBox.Text = this._processWrapper.GetMainWindowTitle();
        groupBox.Location = new System.Drawing.Point(formViewCoordinates.X, formViewCoordinates.Y);
        groupBox.Size = new System.Drawing.Size(300, 150);
        groupBox.FlatStyle = FlatStyle.System; // Apply gray border

        // Create the button
        Button button = new Button();
        button.Text = "Button";
        button.Location = new System.Drawing.Point(20, 30);
        groupBox.Controls.Add(button); // Add button to the group box


        return groupBox;
    }
}