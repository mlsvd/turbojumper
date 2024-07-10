using TurboJumper.Models;

namespace TurboJumper.Decorators;

public interface IProcessListDecorator
{
    public List<ProcessWrapper> Decorate(List<ProcessWrapper> processWrappers);
}
