using Microsoft.VisualBasic;
using TurboJumper.Managers;

namespace TurboJumper.Listeners;

public class KeyboardListener(KeyboardManager keyboardManager)
{
    private KeyboardManager _keyboardManager = keyboardManager;
    public void onKeyDown(object? sender, KeyEventArgs e)
    {
        this._keyboardManager.executeRegisteredAction(e.KeyCode.ToString());
    }
}