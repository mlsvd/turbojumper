using TurboJumper.Models;

namespace TurboJumper.Decorators;

public interface IProcessListDecorator
{
    public List<ProcessWrapper> Provide(List<ProcessWrapper> processWrappers);
}
