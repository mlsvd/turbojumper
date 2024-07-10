using TurboJumper.Models;

namespace TurboJumper.Providers;

public interface IProcessProvider
{
    public List<ProcessWrapper> Provide();
}
