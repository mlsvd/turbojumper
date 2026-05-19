using TurboJumper.Models;
using TurboJumper.Services;

namespace TurboJumper.Decorators;

public class ProcessListWhitelistFilterDecorator(ProcessConfigService processConfigService) : IProcessListDecorator
{
    public List<ProcessWrapper> Decorate(List<ProcessWrapper> processWrappers)
    {
        var config = processConfigService.Load();
        var result = new List<(ProcessWrapper Wrapper, int Order)>();

        foreach (var entry in config.Processes.Where(e => e.Enabled))
        {
            var match = processWrappers.FirstOrDefault(p =>
                string.Equals(p.GetProcessName(), entry.ProcessName, StringComparison.OrdinalIgnoreCase));

            if (match is null) continue;

            match.ConfiguredHotKey = entry.HotKey;
            result.Add((match, entry.Order));
        }

        return result.OrderBy(r => r.Order).Select(r => r.Wrapper).ToList();
    }
}
