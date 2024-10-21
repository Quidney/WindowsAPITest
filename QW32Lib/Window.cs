using QW32Lib.DataTypes.Delegates;
using QW32Lib.DataTypes.Drawing;
using QW32Lib.DataTypes.Helper;
using QW32Lib.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace QW32Lib
{
    public class Window
    {
        public event Action? Shown;
        public event PaintEventDel? Paint;
        public event Action? Destroying;
        public event Action? Destroyed;

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

            hWnd = CreateWindow();
            handleInstancePairs.Add(hWnd, this);
        }

        public static IntPtr WndProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            if (TryGetWindowFromHandle(hWnd, out Window? instance))
            {
                return instance.InstanceWndProc(hWnd, uMsg, wParam, lParam);
            }

            return User32.DefWindowProcW(hWnd, uMsg, wParam, lParam);
        }

        protected virtual IntPtr InstanceWndProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            switch (uMsg)
            {
                case (uint)WindowNotification.WM_CREATE:
                    return IntPtr.Zero;

                case (uint)WindowNotification.WM_PAINT:
                    PAINTSTRUCT ps;
                    User32.BeginPaint(hWnd, out ps);
                    Paint?.Invoke(hWnd, ps.hdc);
                    User32.EndPaint(hWnd, ref ps);
                    return IntPtr.Zero;

                case (uint)WindowNotification.WM_DESTROY:
                    if (TryGetWindowFromHandle(hWnd, out Window? wnd))
                    {
                        wnd.Destroy();
                    }
                    return IntPtr.Zero;

                default:
                    return User32.DefWindowProcW(hWnd, uMsg, wParam, lParam);
            }
        }

        private IntPtr CreateWindow()
        {
            IntPtr hWnd = User32.CreateWindowExW(
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

        public bool Show() => User32.ShowWindow(hWnd, 1);

        public void Destroy()
        {
            Destroying?.Invoke();

            handleInstancePairs.Remove(hWnd);
            User32.DestroyWindow(hWnd);

            Destroyed?.Invoke();

            if (handleInstancePairs.Count == 0)
            {
                User32.PostQuitMessage(0);
            }
        }
    }
}