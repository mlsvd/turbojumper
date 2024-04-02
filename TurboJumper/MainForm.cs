using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TurboJumper;

public partial class MainForm : Form
{
    private TextBox textBox;
    private Dictionary<int, Process> processMap;
    public MainForm()
    {
        InitializeComponent();
        processMap = new Dictionary<int, Process>();
        AddExtraFields();
        RefreshProcesses();
    }

    private void AddExtraFields()
    {
        this.textBox = new TextBox();
        this.textBox.Name = "txtTextArea";
        this.textBox.Multiline = true;
        this.textBox.Size = new System.Drawing.Size(600, 600); // Adjust size as needed
        this.textBox.Location = new System.Drawing.Point(20, 20); // Adjust posit
        this.Controls.Add(this.textBox);
    }
    
    private void RefreshProcesses()
    {
        processMap.Clear();
        // processList.Items.Clear();
        Process[] processes = Process.GetProcesses();
        int index = 1;
        foreach (Process process in processes)
        {
            if (!string.IsNullOrEmpty(process.MainWindowTitle))
            {
                processMap[index] = process;
                Console.WriteLine(process);
                this.textBox.Text += process.MainWindowTitle + "\n";
                if (process.MainWindowTitle == "Untitled - Paint")
                {
                    // this.textBox.Text = "sdf";
                    //this.textBox.Text += process.MainWindowTitle + "\n";
                    // Create a new button
                    Button myButton = new Button();
    
                    // Set properties for the button
                    myButton.Image = GetIconForProcess(process).ToBitmap();//Properties.Resources.YourIcon
                    myButton.ImageAlign = ContentAlignment.MiddleLeft;
                    myButton.Text = process.MainWindowTitle;
                    myButton.Location = new Point(50, 50); // Set the location of the button
                    myButton.Size = new Size(100, 30); // Set the size of the button

                    // Attach an event handler to the button click event
                    // myButton.Click += MyButton_Click;

                    // Add the button to the MainForm's controls collection
                    this.Controls.Add(myButton);
                    
                    // SwitchToProcess(process);
                }
                //processList.Items.Add(process.MainWindowTitle, GetIconForProcess(process));
                index++;
            }
        }
    }
    
    private void SwitchToProcess(Process process)
    {
        IntPtr handle = process.MainWindowHandle;
        if (handle != IntPtr.Zero)
        {
            ShowWindow(handle, SW_RESTORE);
            SetForegroundWindow(handle);
        }
    }
    
    private Icon GetIconForProcess(Process process)
    {
        try
        {
            Icon icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
            return icon;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private const int SW_RESTORE = 9;

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
}