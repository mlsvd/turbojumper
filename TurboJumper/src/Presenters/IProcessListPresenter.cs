using TurboJumper.Models;

namespace TurboJumper.Presenters;

public interface IProcessListPresenter
{
    public void Present(Control container, List<ProcessWrapper> processWrappers);
}
