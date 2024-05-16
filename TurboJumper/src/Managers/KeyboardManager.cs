using TurboJumper.Handlers;
using TurboJumper.Models;

namespace TurboJumper.Managers;

public class KeyboardManager(SwitchToProcessHandler switchToProcessHandler)
{
    private SwitchToProcessHandler _switchToProcessHandler = switchToProcessHandler;
    private Dictionary<string, KeyboardRegistryEntry> _keyboardRegistryEntries = new Dictionary<string, KeyboardRegistryEntry>();

    public void registerCombination(string combination, KeyboardRegistryEntry registryEntry)
    {
        this._keyboardRegistryEntries[combination] = registryEntry;
    }
    
    public bool hasRegisteredKeyCombination(string combination)
    {
        return this._keyboardRegistryEntries.ContainsKey(combination);
    }

    public void executeRegisteredAction(string combination)
    {
        if (!this.hasRegisteredKeyCombination(combination))
        {
            Console.WriteLine("Combination doesnt exist");
            Console.WriteLine(combination);
            return;
        }

        var registryEntry = this._keyboardRegistryEntries[combination];
        switch (registryEntry.Action)
        {
            case "switchToProcess":
                Console.WriteLine("switchToProcess");
                if (registryEntry.ProcessHandle != null)
                {
                    IntPtr handle = registryEntry.ProcessHandle.Value;
                    this._switchToProcessHandler.SwitchToProcessByHandle(handle);                    
                }
                
                break;
        }
        
    }

}
