using QW32Lib.DataTypes;
using QW32Lib.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace QW32Lib
{
    public class Window
    {
        public static bool TryGetWindowFromHandle(IntPtr hWnd, [NotNullWhen(true)] out Window? instance) => handleInstancePairs.TryGetValue(hWnd, out instance);


        static Dictionary<IntPtr, Window> handleInstancePairs = [];
        static List<Window> OpenWindows = [];

        private IntPtr hWnd;

        private string Title;
        private int StartPosX;
        private int StartPosY;
        private int Width;
        private int Height;

        WindowClass WndClass;
        private string ClassName => WindowClass.ClassName;
        private IntPtr hInstance => WndClass.HInstance;


        public Window(string windowName, int width, int height, WindowClass wndClass, bool centerToScreen = false)
        {
            WndClass = wndClass;
            Title = windowName;
            Width = width;
            Height = height;

            if (centerToScreen)
            {
                int sWidth = User32.GetSystemMetrics(SystemMetrics.SM_CXSCREEN);
                int sHeight = User32.GetSystemMetrics(SystemMetrics.SM_CYSCREEN);

                StartPosX = (sWidth / 2) - (width / 2);
                StartPosY = (sHeight / 2) - (height / 2);
            }

            Create();

            OpenWindows.Add(this);
            handleInstancePairs.Add(hWnd, this);
        }

        public IntPtr Create()
        {
            hWnd = User32.CreateWindowExW(
                dwExStyle: 0x00000000,
                    lpClassName: ClassName,
                    lpWindowName: Title,
                    dwStyle: (int)WindowStyle.WS_OVERLAPPEDWINDOW,
                    X: StartPosX,
                    Y: StartPosY,
                    nWidth: Width,
                    nHeight: Height,
                    hWndParent: IntPtr.Zero,
                    hMenu: IntPtr.Zero,
                    hInstance: hInstance,
                    lpParam: IntPtr.Zero
                );

            if (hWnd == IntPtr.Zero)
            {
                int error = Marshal.GetLastWin32Error();
                throw new InvalidOperationException($"Cannot Create Window. Win32 Error Code: {error}");
            }

            return hWnd;
        }

        public bool Show()
        {
            return User32.ShowWindow(hWnd, 1);
        }

        public static void MessageLoop()
        {
            while (User32.GetMessageW(out MSG msg, IntPtr.Zero, 0, 0))
            {
                User32.TranslateMessage(ref msg);
                User32.DispatchMessageW(ref msg);
            }
        }

        public void Destroy()
        {
            OpenWindows.Remove(this);
            handleInstancePairs.Remove(hWnd);

            User32.DestroyWindow(hWnd);

            if (OpenWindows.Count == 0)
            {
                User32.PostQuitMessage(0);
            }
        }
    }
}