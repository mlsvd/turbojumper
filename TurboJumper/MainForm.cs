using TurboJumper.Listeners;
using TurboJumper.Presenters;
using TurboJumper.Providers;
using Timer = System.Windows.Forms.Timer;

namespace TurboJumper;

public partial class MainForm: Form
{
    private IProcessProvider _processProvider;
    private IProcessListPresenter _processListPresenter;
    private IAppConfigurationProvider _configuration;
    private KeyboardListener _keyboardListener;
    private Timer timer;
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
        
        this.Text = "TurboJumper";
        this.Icon = new Icon("src\\Resources\\Images\\app.ico");

        KeyPreview = true;
        KeyDown += this._keyboardListener.onKeyDown;
        KeyDown += this.MainForm_KeyDown;

        this.Width = this._configuration.ProvideIntValue("FormView:FormWidth", 800);
        this.Height = this._configuration.ProvideIntValue("FormView:FormHeight", 700);

        DisplayProcessList();

    }

    private void DisplayProcessList()
    {
        var processWrapperList = this._processProvider.Provide();
        this._processListPresenter.Present(this, processWrapperList);
    }
    
    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        // Check if the space key was pressed
        if (e.KeyCode == Keys.Space)
        {
            this.DisplayProcessList();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }
}
