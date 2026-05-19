using System.Diagnostics;
using TurboJumper.Models;
using TurboJumper.Providers;
using TurboJumper.Services;

namespace TurboJumper.Forms;

public class ConfigureForm : Form
{
    private readonly ProcessConfigService _configService;
    private readonly ProcessProvider _processProvider;

    private DataGridView _grid = null!;
    private DataGridViewCheckBoxColumn _colEnabled = null!;
    private DataGridViewTextBoxColumn _colOrder = null!;
    private DataGridViewTextBoxColumn _colProcessName = null!;
    private DataGridViewTextBoxColumn _colDisplayName = null!;
    private DataGridViewComboBoxColumn _colHotKey = null!;

    // Maps display label -> stored key code, ordered for the dropdown
    private static readonly (string Label, string Code)[] HotKeyOptions =
    [
        ("1", "D1"), ("2", "D2"), ("3", "D3"), ("4", "D4"), ("5", "D5"),
        ("6", "D6"), ("7", "D7"), ("8", "D8"), ("9", "D9"),
        ("Q", "q"), ("W", "w"), ("E", "e"), ("R", "r"), ("T", "t"),
        ("Y", "y"), ("U", "u"), ("I", "i"), ("O", "o"), ("P", "p"),
        ("A", "a"), ("S", "s"), ("D", "d"), ("F", "f"), ("G", "g"),
        ("H", "h"), ("J", "j"), ("K", "k"), ("L", "l"),
        ("Z", "z"), ("X", "x"), ("C", "c"), ("V", "v"), ("B", "b"),
        ("N", "n"), ("M", "m"),
        ("(none)", ""),
    ];

    public ConfigureForm(ProcessConfigService configService, ProcessProvider processProvider)
    {
        _configService = configService;
        _processProvider = processProvider;

        Text = "Configure TurboJumper";
        Size = new Size(720, 520);
        MinimumSize = new Size(620, 400);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.Sizable;

        BuildLayout();
        LoadConfig();
    }

    private void BuildLayout()
    {
        _colEnabled = new DataGridViewCheckBoxColumn
        {
            HeaderText = "On",
            Name = "Enabled",
            Width = 36,
            TrueValue = true,
            FalseValue = false,
        };

        _colOrder = new DataGridViewTextBoxColumn
        {
            HeaderText = "Order",
            Name = "Order",
            Width = 56,
        };

        _colProcessName = new DataGridViewTextBoxColumn
        {
            HeaderText = "Process Name",
            Name = "ProcessName",
            Width = 130,
            ToolTipText = "Exact process name (e.g. chrome, msedge, Code)",
        };

        _colDisplayName = new DataGridViewTextBoxColumn
        {
            HeaderText = "Display Name",
            Name = "DisplayName",
            Width = 130,
        };

        _colHotKey = new DataGridViewComboBoxColumn
        {
            HeaderText = "Hot Key",
            Name = "HotKey",
            Width = 80,
            DisplayMember = "Label",
            ValueMember = "Code",
            DataSource = HotKeyOptions.Select(x => new { x.Label, x.Code }).ToList(),
            FlatStyle = FlatStyle.Flat,
        };

        _grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            RowHeadersVisible = false,
            MultiSelect = false,
            EditMode = DataGridViewEditMode.EditOnEnter,
            BackgroundColor = SystemColors.Window,
            BorderStyle = BorderStyle.None,
        };
        _grid.Columns.AddRange(_colEnabled, _colOrder, _colProcessName, _colDisplayName, _colHotKey);

        // Bottom toolbar
        var bottomPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 44,
            Padding = new Padding(6, 6, 6, 6),
            FlowDirection = FlowDirection.LeftToRight,
        };

        var btnAddEmpty = MakeButton("Add Row", OnAddEmpty);
        var btnAddRunning = MakeButton("Add Running...", OnAddRunning);
        var btnRemove = MakeButton("Remove Selected", OnRemove);

        var spacer = new Panel { Width = 120 };

        var btnSave = MakeButton("Save", OnSave);
        var btnCancel = MakeButton("Cancel", (_, _) => { DialogResult = DialogResult.Cancel; Close(); });

        bottomPanel.Controls.AddRange([btnAddEmpty, btnAddRunning, btnRemove, spacer, btnSave, btnCancel]);

        Controls.Add(_grid);
        Controls.Add(bottomPanel);
    }

    private static Button MakeButton(string text, EventHandler handler)
    {
        var btn = new Button { Text = text, AutoSize = true, Height = 28 };
        btn.Click += handler;
        return btn;
    }

    private void LoadConfig()
    {
        var config = _configService.Load();
        _grid.Rows.Clear();

        foreach (var entry in config.Processes.OrderBy(e => e.Order))
            AddRow(entry);
    }

    private void AddRow(ProcessConfigEntry entry)
    {
        var hotKeyCode = HotKeyOptions.Any(h => h.Code == entry.HotKey) ? entry.HotKey : "";
        _grid.Rows.Add(entry.Enabled, entry.Order, entry.ProcessName, entry.DisplayName, hotKeyCode);
    }

    private void OnAddEmpty(object? sender, EventArgs e)
    {
        var nextOrder = _grid.Rows.Count > 0
            ? _grid.Rows.Cast<DataGridViewRow>()
                .Select(r => r.Cells[_colOrder.Index].Value as string)
                .Where(v => int.TryParse(v, out _))
                .Select(v => int.Parse(v!))
                .DefaultIfEmpty(0)
                .Max() + 1
            : 1;

        _grid.Rows.Add(true, nextOrder.ToString(), "", "", "");
    }

    private void OnAddRunning(object? sender, EventArgs e)
    {
        List<string> running;
        try
        {
            running = _processProvider.Provide()
                .Where(p => p.IsMainProcess())
                .Select(p => p.GetProcessName())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(n => n)
                .ToList();
        }
        catch
        {
            MessageBox.Show("Could not retrieve running processes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var existingNames = _grid.Rows.Cast<DataGridViewRow>()
            .Select(r => r.Cells[_colProcessName.Index].Value?.ToString() ?? "")
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var available = running.Where(n => !existingNames.Contains(n)).ToList();
        if (available.Count == 0)
        {
            MessageBox.Show("All running processes are already in the list.", "Nothing to add",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var picker = new RunningProcessPickerForm(available);
        if (picker.ShowDialog(this) != DialogResult.OK || picker.SelectedProcessName is null) return;

        var nextOrder = _grid.Rows.Count > 0
            ? _grid.Rows.Cast<DataGridViewRow>()
                .Select(r => r.Cells[_colOrder.Index].Value as string)
                .Where(v => int.TryParse(v, out _))
                .Select(v => int.Parse(v!))
                .DefaultIfEmpty(0)
                .Max() + 1
            : 1;

        _grid.Rows.Add(true, nextOrder.ToString(), picker.SelectedProcessName, picker.SelectedProcessName, "");
    }

    private void OnRemove(object? sender, EventArgs e)
    {
        if (_grid.SelectedRows.Count == 0) return;
        _grid.Rows.Remove(_grid.SelectedRows[0]);
    }

    private void OnSave(object? sender, EventArgs e)
    {
        // Commit any pending edits
        _grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        _grid.EndEdit();

        var entries = new List<ProcessConfigEntry>();
        var usedKeys = new HashSet<string>();

        for (int i = 0; i < _grid.Rows.Count; i++)
        {
            var row = _grid.Rows[i];
            var processName = row.Cells[_colProcessName.Index].Value?.ToString()?.Trim() ?? "";
            if (string.IsNullOrEmpty(processName)) continue;

            var enabled = row.Cells[_colEnabled.Index].Value is true;
            var orderStr = row.Cells[_colOrder.Index].Value?.ToString() ?? "";
            int.TryParse(orderStr, out int order);
            var displayName = row.Cells[_colDisplayName.Index].Value?.ToString()?.Trim() ?? processName;
            var hotKey = row.Cells[_colHotKey.Index].Value?.ToString() ?? "";

            if (enabled && !string.IsNullOrEmpty(hotKey))
            {
                if (!usedKeys.Add(hotKey))
                {
                    MessageBox.Show(
                        $"Hot key conflict: key '{hotKey}' is assigned to more than one enabled process. Please resolve before saving.",
                        "Hot Key Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            entries.Add(new ProcessConfigEntry
            {
                ProcessName = processName,
                DisplayName = displayName,
                HotKey = hotKey,
                Order = order == 0 ? i + 1 : order,
                Enabled = enabled,
            });
        }

        _configService.Save(new AppProcessConfig { Processes = entries });
        DialogResult = DialogResult.OK;
        Close();
    }
}

internal class RunningProcessPickerForm : Form
{
    private readonly ListBox _listBox;
    public string? SelectedProcessName { get; private set; }

    public RunningProcessPickerForm(List<string> processNames)
    {
        Text = "Select Running Process";
        Size = new Size(300, 380);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        _listBox = new ListBox
        {
            Dock = DockStyle.Fill,
            SelectionMode = SelectionMode.One,
        };
        _listBox.Items.AddRange(processNames.Cast<object>().ToArray());
        _listBox.DoubleClick += OnConfirm;

        var bottomPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 40,
            Padding = new Padding(6, 4, 6, 4),
            FlowDirection = FlowDirection.RightToLeft,
        };

        var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Height = 28, AutoSize = true };
        var btnOk = new Button { Text = "Add", Height = 28, AutoSize = true };
        btnOk.Click += OnConfirm;

        bottomPanel.Controls.AddRange([btnCancel, btnOk]);

        Controls.Add(_listBox);
        Controls.Add(bottomPanel);
    }

    private void OnConfirm(object? sender, EventArgs e)
    {
        if (_listBox.SelectedItem is string selected)
        {
            SelectedProcessName = selected;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
