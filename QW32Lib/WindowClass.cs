using QW32Lib.DataTypes.Delegates;
using QW32Lib.DataTypes.Drawing;
using QW32Lib.DataTypes.Helper;
using QW32Lib.Enums;
using System.Reflection;
using System.Runtime.InteropServices;

namespace QW32Lib
{
    public class WindowClass
    {
        private const string DefaultClassName = "QW32WindowClass";

        public static WindowClass Default => defaultInstance.Value;
        private static readonly Lazy<WindowClass> defaultInstance = new(() => new WindowClass(className: DefaultClassName));

        public static WindowClass GetInstance(IntPtr hInstance)
        {
            if (!handleWndClassDictionary.TryGetValue(hInstance, out WindowClass? wndClass))
                throw new KeyNotFoundException($"Given handle does not correspond to a {nameof(WindowClass)} instance.");

            return wndClass;
        }
        private static Dictionary<IntPtr, WindowClass> handleWndClassDictionary = [];

        public IntPtr HInstance { get; }
        public string ClassName { get; }


        private WndProcDelegate dWndProc;
        private IntPtr lpfnWndProc;

        private WNDCLASSEXW WNDCLASSEXW;

        public WindowClass(string className)
        {
            ClassName = className;

            dWndProc = WndProc;

            lpfnWndProc = Marshal.GetFunctionPointerForDelegate(dWndProc);
            HInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);

            WNDCLASSEXW = CreateWNDCLASSEXW();
            RegisterClass();

            handleWndClassDictionary.Add(HInstance, this);
        }

        protected virtual IntPtr WndProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            switch (uMsg)
            {
                case WindowNotification.WM_CREATE:
                    break;

                case WindowNotification.WM_DESTROY:
                    if (Window.TryGetWindowFromHandle(hWnd, out Window? window))
                    {
                        window.Destroy();
                    }
                    break;

                case WindowNotification.WM_PAINT:
                    {
                        IntPtr hdc = User32.BeginPaint(hWnd, out PAINTSTRUCT ps);

                        User32.EndPaint(hwnd, ps);
                    }

                default:
                    return User32.DefWindowProcW(hWnd, uMsg, wParam, lParam);
            }

            return IntPtr.Zero;
        }

        private unsafe WNDCLASSEXW CreateWNDCLASSEXW() => new()
        {
            cbSize = (uint)sizeof(WNDCLASSEXW),
            style = 0x00000000,
            lpfnWndProc = lpfnWndProc,
            cbClsExtra = 0,
            cbWndExtra = 0,
            hInstance = HInstance,
            hIcon = IntPtr.Zero,
            hCursor = User32.LoadCursorW(hInstance: IntPtr.Zero, lpCursorName: (IntPtr)Cursor.IDC_ARROW),
            hbrBackground = IntPtr.Zero,
            lpszMenuName = IntPtr.Zero,
            lpszClassName = Marshal.StringToHGlobalUni(ClassName),
            hIconSm = IntPtr.Zero
        };

        private IntPtr RegisterClass() => User32.RegisterClassExW(WNDCLASSEXW);
    }
}