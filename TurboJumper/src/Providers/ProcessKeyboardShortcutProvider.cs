using TurboJumper.Models;

namespace TurboJumper.Providers;

public class ProcessKeyboardShortcutProvider
{
    Dictionary<string, string> keyMap = new Dictionary<string, string>
    {
        { "D1", "key 1" },
        { "D2", "key 2" },
        { "D3", "key 3" },
        { "D4", "key 4" },
        { "D5", "key 5" },
        { "D6", "key 6" },
        { "D7", "key 7" },
        { "D8", "key 8" },
        { "D9", "key 9" },
        { "q", "key q" },
        { "w", "key w" },
        { "e", "key e" },
        { "r", "key r" },
        { "t", "key t" },
        { "y", "key y" },
        { "u", "key u" },
        { "i", "key i" },
        { "o", "key o" },
        { "p", "key p" },
        { "a", "key a" },
        { "s", "key s" },
        { "d", "key d" },
        { "f", "key f" },
        { "g", "key g" },
        { "h", "key h" },
        { "j", "key j" },
        { "k", "key k" },
        { "l", "key l" },
        { "z", "key z" },
        { "x", "key x" },
        { "c", "key c" },
        { "v", "key v" },
        { "b", "key b" },
        { "n", "key n" },
        { "m", "key m" },
    };
    public ShortcutConfig ProvideForProcess(ProcessWrapper processWrapper, int index = -1)
    {
        var shortcut = new ShortcutConfig();
        shortcut.HotKey = "n/a";
        shortcut.DisplayName = "n/a";

        if (index >= 0 && index < keyMap.Count)
        {
            shortcut.HotKey = this.keyMap.ElementAt(index).Key;
            shortcut.DisplayName = this.keyMap.ElementAt(index).Value;
        }

        return shortcut;
    }
}
