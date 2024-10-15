using QW32Lib;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace WindowsAPITest
{
    internal class Program
    {
        [SupportedOSPlatform(nameof(OSPlatform.Windows))]
        static void Main()
        {
            Window window = new();
            window.Show();

            Application.Run();
        }
    }
}
