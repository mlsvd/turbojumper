using TurboJumper.Managers;
using TurboJumper.Models;
using TurboJumper.Providers;

namespace TurboJumper.Decorators;

public class ProcessListKeyCombinationDecorator(
    KeyboardManager keyboardManager, 
    ProcessKeyboardShortcutProvider keyboardShortcutProvider
): IProcessListDecorator
{
    private KeyboardManager _keyboardManager = keyboardManager;
    private ProcessKeyboardShortcutProvider _keyboardShortcutProvider = keyboardShortcutProvider;

    public List<ProcessWrapper> Decorate(List<ProcessWrapper> processWrappers)
    {
        int index = 1;
        foreach (ProcessWrapper processWrapper in processWrappers)
        {
            this._keyboardManager.registerCombination(
                this._keyboardShortcutProvider.ProvideForProcess(processWrapper, index),
                new KeyboardRegistryEntry
                {
                    Action = "switchToProcess", 
                    ProcessHandle = processWrapper.GetMainWindowHandle()
                }
            );

            index++;
        }
        
        Console.WriteLine(processWrappers);

        return processWrappers;
    }
}
