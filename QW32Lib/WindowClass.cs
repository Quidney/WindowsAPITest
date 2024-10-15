using QW32Lib.DataTypes.Delegates;
using QW32Lib.DataTypes.Helper;
using QW32Lib.Enums;
using System.Reflection;
using System.Runtime.InteropServices;

namespace QW32Lib
{
    public class WindowClass
    {
        private const string DefaultClassName = "QW32WindowClass";

        public static WindowClass Default => @default.Value;
        private static Lazy<WindowClass> @default = new(() => new WindowClass(className: DefaultClassName));

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

            Create();
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

                default:
                    return User32.DefWindowProcW(hWnd, uMsg, wParam, lParam);
            }

            return IntPtr.Zero;
        }

        private unsafe WNDCLASSEXW Create()
        {
            WNDCLASSEXW.cbSize = (uint)sizeof(WNDCLASSEXW);
            WNDCLASSEXW.style = 0x00000000;
            WNDCLASSEXW.lpfnWndProc = lpfnWndProc;
            WNDCLASSEXW.cbClsExtra = 0;
            WNDCLASSEXW.cbWndExtra = 0;
            WNDCLASSEXW.hInstance = HInstance;
            WNDCLASSEXW.hIcon = IntPtr.Zero;
            WNDCLASSEXW.hCursor = User32.LoadCursorW(hInstance: IntPtr.Zero, lpCursorName: (IntPtr)Cursor.IDC_ARROW);
            WNDCLASSEXW.hbrBackground = IntPtr.Zero;
            WNDCLASSEXW.lpszMenuName = IntPtr.Zero;
            WNDCLASSEXW.lpszClassName = Marshal.StringToHGlobalUni(ClassName);
            WNDCLASSEXW.hIconSm = IntPtr.Zero;

            return WNDCLASSEXW;
        }
        private IntPtr RegisterClass() => User32.RegisterClassExW(WNDCLASSEXW);
    }
}