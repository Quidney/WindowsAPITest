using QW32Lib;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace WindowsAPITest
{
    delegate IntPtr WndProcDel(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam);

    internal class Program
    {
        [SupportedOSPlatform(nameof(OSPlatform.Windows))]
        static void Main()
        {
            WindowClass wndClass = new();

            Window window = new(wndClass.HInstance, centerToScreen: true);
            window.Show();

            Window.MessageLoop();
        }
    }
}
