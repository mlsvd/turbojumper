using TurboJumper.Models;

namespace TurboJumper.Presenters;

public interface IProcessListPresenter
{
    public Form Present(Form targetForm, List<ProcessWrapper> processWrappers);
}
