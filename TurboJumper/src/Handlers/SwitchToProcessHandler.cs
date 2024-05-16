using System.Runtime.InteropServices;
using TurboJumper.Models;

namespace TurboJumper.Handlers;

public class SwitchToProcessHandler
{
    public void SwitchToProcess(ProcessWrapper processWrapper)
    {
        if (processWrapper is not ProcessWrapper)
        {
            return;
        }
        IntPtr handle = processWrapper.GetMainWindowHandle();
        this.SwitchToProcessByHandle(handle);
    }

    public void SwitchToProcessByHandle(IntPtr handle)
    {
        if (handle != IntPtr.Zero)
        {
            // ShowWindow(handle, SW_RESTORE);
            // SetForegroundWindow(handle);
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            GetWindowPlacement(handle, ref placement);
        
            Console.WriteLine("Placement ");
            Console.WriteLine(placement.showCmd);
            // Check if the window is minimized
            if (placement.showCmd == 2) // 2 represents SW_SHOWMINIMIZED
            {
                Console.WriteLine("Restore from minimized");
                ShowWindow(handle, SW_RESTORE);
                SetForegroundWindow(handle);
            }
            else
            {
                Console.WriteLine("Show window");
                ShowWindow(handle, SW_SHOW);
                SetForegroundWindow(handle);
            }
        }
    }

private const int SW_RESTORE = 9;
    private const int SW_SHOW = 5;
    
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    private struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public int showCmd;
        public System.Drawing.Point ptMinPosition;
        public System.Drawing.Point ptMaxPosition;
        public System.Drawing.Rectangle rcNormalPosition;
    }
    
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    
    [DllImport("user32.dll")]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    
    [DllImport("user32.dll")]
    private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
    
    [DllImport("user32.dll")]
    private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
}
