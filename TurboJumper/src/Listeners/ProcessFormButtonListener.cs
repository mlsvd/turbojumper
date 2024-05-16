
using TurboJumper.Handlers;
using TurboJumper.Models;

namespace TurboJumper.Listeners;

public class ProcessFormButtonListener(SwitchToProcessHandler switchToProcessHandler)
{
    private SwitchToProcessHandler _switchToProcessHandler = switchToProcessHandler;
    
    public void OnClick(object sender, EventArgs e)
    {
        ProcessWrapper processWrapper = ((Button)sender).Tag as ProcessWrapper;

        this._switchToProcessHandler.SwitchToProcess(processWrapper);
    }
}