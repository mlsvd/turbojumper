using TurboJumper.Models;

namespace TurboJumper.Providers;

public class ProcessKeyboardShortcutProvider
{
    public string ProvideForProcess(ProcessWrapper processWrapper, int index = -1)
    {
        if (index > 0 && index <= 9)
        {
            return "D" + index.ToString();                
            
        }

        return "n_a";
    }
}