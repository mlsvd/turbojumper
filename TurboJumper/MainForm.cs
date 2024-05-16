using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TurboJumper.Listeners;
using TurboJumper.Managers;
using TurboJumper.Presenters;
using TurboJumper.Providers;

namespace TurboJumper;

public partial class MainForm: Form
{
    private IProcessProvider _processProvider;
    private IProcessListPresenter _processListPresenter;
    private IAppConfigurationProvider _configuration;
    private KeyboardListener _keyboardListener;
    public MainForm(
        IProcessProvider processProvider, 
        IProcessListPresenter processListPresenter, 
        IAppConfigurationProvider configuration,
        KeyboardListener keyboardListener
    )
    {
        this._processProvider = processProvider;
        this._processListPresenter = processListPresenter;
        this._configuration = configuration;
        this._keyboardListener = keyboardListener;

        KeyPreview = true;
        KeyDown += this._keyboardListener.onKeyDown;

        this.Width = this._configuration.ProvideIntValue("FormView:FormWidth", 800);
        this.Height = this._configuration.ProvideIntValue("FormView:FormHeight", 700);

        DisplayProcessList();
   }

    private void DisplayProcessList()
    {
        var processWrapperList = this._processProvider.Provide();
        this._processListPresenter.Present(this, processWrapperList);
    }
}
