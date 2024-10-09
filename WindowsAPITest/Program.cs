using System.Reflection;
using System.Runtime.InteropServices;

namespace WindowsAPITest
{
    internal partial class User32
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal extern static IntPtr RegisterClassExA(ref WNDCLASSEXW lpwcx);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal extern static IntPtr CreateWindowExW(
            uint dwExStyle,
            [MarshalAs(UnmanagedType.LPWStr)] string lpClassName,
            [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName,
            uint dwStyle,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam
         );

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal extern static bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WNDCLASSEXW
    {
        public uint cbSize;
        public uint style;
        [MarshalAs(UnmanagedType.FunctionPtr)] public IntPtr lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        [MarshalAs(UnmanagedType.LPWStr)] public IntPtr lpszMenuName;
        [MarshalAs(UnmanagedType.LPWStr)] public IntPtr lpszClassName;
        public IntPtr hIconSm;
    }


    internal class Program
    {
        delegate IntPtr WndProc(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        static void Main()
        {
            try
            {
                WndProc WindwProc = Wndproc;

                WNDCLASSEXW wndClass = new()
                {
                    lpfnWndProc = Marshal.GetFunctionPointerForDelegate(WindwProc),
                    hInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                    lpszClassName = Marshal.StringToHGlobalUni("QuidneysWindows"),
                };

                User32.RegisterClassExA(ref wndClass);

                IntPtr hWnd = User32.CreateWindowExW(
                    0,
                    "QuidneysWindows",
                    "Quidneys Windows",
                    0x00CF0000,
                    0,
                    0,
                    800,
                    600,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    wndClass.hInstance,
                    IntPtr.Zero
                );

                if (hWnd == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to create window.");
                    Console.WriteLine(Marshal.GetLastWin32Error());
                    return;
                }

                User32.ShowWindow(hWnd, 1);
            }
            catch
            {
                Console.WriteLine(Marshal.GetLastWin32Error());
            }
        }

        static IntPtr Wndproc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            switch (uMsg)
            {
                case 0x0001:
                    break;
            }

            return IntPtr.Zero;
        }
    }
}
