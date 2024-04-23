using TurboJumper.Models;

namespace TurboJumper.Presenters;

public class ProcessListPresenter: IProcessListPresenter
{
    public Form Present(Form targetForm, List<ProcessWrapper> processWrappers)
    {
        var formViewCoordinates = FormViewCoordinates.createFromForm(targetForm);

        foreach (ProcessWrapper processWrapper in processWrappers)
        {
            var elementPresenter = ProcessWrapperElementPresenter.CreateFromProcessWrapper(processWrapper);
            targetForm.Controls.Add(elementPresenter.toFormElement(formViewCoordinates));

            formViewCoordinates.X += 305;
            formViewCoordinates.Y += 150;
        }
        
        return targetForm;
    }
}
