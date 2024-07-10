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
        this._keyboardManager.resetCombinations();
        
        int index = 0;
        foreach (ProcessWrapper processWrapper in processWrappers)
        {
            var shortcutConfig = this._keyboardShortcutProvider.ProvideForProcess(processWrapper, index);
            processWrapper.KeyboardShortcut = shortcutConfig;
            this._keyboardManager.registerCombination(
                shortcutConfig.HotKey,
                new KeyboardRegistryEntry
                {
                    Action = "switchToProcess", 
                    ProcessHandle = processWrapper.GetMainWindowHandle()
                }
            );

            index++;
        }

        return processWrappers;
    }
}
