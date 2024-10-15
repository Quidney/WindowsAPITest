using QW32Lib.Enums;
using QW32Lib.Helper;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace QW32Lib
{
    public class Window
    {
        public static bool TryGetWindowFromHandle(IntPtr hWnd, [NotNullWhen(true)] out Window? instance) => handleInstancePairs.TryGetValue(hWnd, out instance);
        private static Dictionary<IntPtr, Window> handleInstancePairs = [];

        private IntPtr hWnd;
        private WindowConfig Config;


        public Window(bool centerToScreen = true) : this(WindowClass.Default.HInstance, centerToScreen: centerToScreen)
        {
        }

        /// <summary>
        /// Calls the Constructor with WindowConfig
        /// </summary>
        /// <param name="hInstance"></param>
        /// <param name="dwExStyle"></param>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <param name="dwStyle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="hWndParent"></param>
        /// <param name="hMenu"></param>
        /// <param name="lpParam"></param>
        /// <param name="centerToScreen"></param>
        public Window(
            IntPtr hInstance,
            uint dwExStyle = 0x00000000,
            string lpWindowName = "QW32 Window",
            uint dwStyle = (int)WindowStyle.WS_OVERLAPPEDWINDOW,
            int x = 0,
            int y = 0,
            int nWidth = 960,
            int nHeight = 540,
            IntPtr hWndParent = default,
            IntPtr hMenu = default,
            IntPtr lpParam = default,
            bool centerToScreen = false)

            : this(
                new WindowConfig(
                    _dwExStyle: dwExStyle,
                    _lpClassName: WindowClass.GetInstance(hInstance).ClassName,
                    _lpWindowName: lpWindowName,
                    _dwStyle: dwStyle,
                    _X: x,
                    _Y: y,
                    _nWidth: nWidth,
                    _nHeight: nHeight,
                    _hWndParent: hWndParent,
                    _hMenu: hMenu,
                    _hInstance: hInstance,
                    _lpParam: lpParam),
                centerToScreen: centerToScreen)
        {
        }

        public Window(WindowConfig config, bool centerToScreen = false)
        {
            Config = config;

            if (centerToScreen)
            {
                int sWidth = User32.GetSystemMetrics(SystemMetrics.SM_CXSCREEN);
                int sHeight = User32.GetSystemMetrics(SystemMetrics.SM_CYSCREEN);

                Config.X = (sWidth / 2) - (Config.nWidth / 2);
                Config.Y = (sHeight / 2) - (Config.nHeight / 2);
            }

            Create();

            handleInstancePairs.Add(hWnd, this);
        }

        private IntPtr Create()
        {
            hWnd = User32.CreateWindowExW(
                dwExStyle: Config.dwExStyle,
                lpClassName: Config.lpClassName,
                lpWindowName: Config.lpWindowName,
                dwStyle: Config.dwStyle,
                X: Config.X,
                Y: Config.Y,
                nWidth: Config.nWidth,
                nHeight: Config.nHeight,
                hWndParent: Config.hWndParent,
                hMenu: Config.hMenu,
                hInstance: Config.hInstance,
                lpParam: Config.lpParam
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

        public void Destroy()
        {
            handleInstancePairs.Remove(hWnd);
            User32.DestroyWindow(hWnd);

            if (handleInstancePairs.Count == 0)
            {
                User32.PostQuitMessage(0);
            }
        }
    }
}