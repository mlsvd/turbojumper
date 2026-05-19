using TurboJumper.Forms;
using TurboJumper.Listeners;
using TurboJumper.Presenters;
using TurboJumper.Providers;
using TurboJumper.Services;
using Timer = System.Windows.Forms.Timer;

namespace TurboJumper;

public partial class MainForm : Form
{
    private readonly IProcessProvider _processProvider;
    private readonly IProcessListPresenter _processListPresenter;
    private readonly IAppConfigurationProvider _configuration;
    private readonly KeyboardListener _keyboardListener;
    private readonly ProcessConfigService _processConfigService;
    private readonly ProcessProvider _rawProcessProvider;

    private Panel _processListPanel = null!;

    public MainForm(
        IProcessProvider processProvider,
        IProcessListPresenter processListPresenter,
        IAppConfigurationProvider configuration,
        KeyboardListener keyboardListener,
        ProcessConfigService processConfigService,
        ProcessProvider rawProcessProvider
    )
    {
        _processProvider = processProvider;
        _processListPresenter = processListPresenter;
        _configuration = configuration;
        _keyboardListener = keyboardListener;
        _processConfigService = processConfigService;
        _rawProcessProvider = rawProcessProvider;

        Text = "TurboJumper";
        Icon = new Icon(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src", "Resources", "Images", "app.ico"));
        KeyPreview = true;
        Width = _configuration.ProvideIntValue("FormView:FormWidth", 800);
        Height = _configuration.ProvideIntValue("FormView:FormHeight", 700);

        BuildLayout();

        KeyDown += _keyboardListener.onKeyDown;
        KeyDown += MainForm_KeyDown;

        DisplayProcessList();
    }

    private void BuildLayout()
    {
        var toolbar = new Panel
        {
            Dock = DockStyle.Top,
            Height = 40,
            BackColor = SystemColors.Control,
        };

        var configureButton = new Button
        {
            Text = "Configure",
            Size = new Size(100, 28),
            Location = new Point(8, 6),
        };
        configureButton.Click += OnConfigure;
        toolbar.Controls.Add(configureButton);

        _processListPanel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
        };

        // Add fill panel before top panel so dock order is correct
        Controls.Add(_processListPanel);
        Controls.Add(toolbar);
    }

    private void DisplayProcessList()
    {
        var processWrapperList = _processProvider.Provide();
        _processListPresenter.Present(_processListPanel, processWrapperList);
    }

    private void OnConfigure(object? sender, EventArgs e)
    {
        using var form = new ConfigureForm(_processConfigService, _rawProcessProvider);
        if (form.ShowDialog(this) == DialogResult.OK)
            DisplayProcessList();
    }

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            DisplayProcessList();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }
}
