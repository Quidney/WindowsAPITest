using QW32Lib.DataTypes;
using QW32Lib.DataTypes.Delegates;
using QW32Lib.Enums;
using System.Reflection;
using System.Runtime.InteropServices;

namespace QW32Lib
{
    public class WindowClass
    {
        public const string ClassName = "QW32WindowClass";

        public IntPtr HInstance => hInstance;
        private IntPtr hInstance;

        public IntPtr LpfnWndProc => lpfnWndProc;
        private IntPtr lpfnWndProc;

        private WndProcDelegate dWndProc;
        private WNDCLASSEXW WNDCLASSEXW;

        public WindowClass()
        {
            dWndProc = WndProc;

            lpfnWndProc = Marshal.GetFunctionPointerForDelegate(dWndProc);
            hInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);

            Create();
            RegisterClass();
        }

        private IntPtr WndProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            switch (uMsg)
            {
                case WindowNotifications.WM_CREATE:
                    break;

                case WindowNotifications.WM_DESTROY:
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
            WNDCLASSEXW.hInstance = hInstance;
            WNDCLASSEXW.hIcon = IntPtr.Zero;
            WNDCLASSEXW.hCursor = User32.LoadCursorW(hInstance: IntPtr.Zero, lpCursorName: (IntPtr)Cursor.IDC_ARROW);
            WNDCLASSEXW.hbrBackground = IntPtr.Zero;
            WNDCLASSEXW.lpszMenuName = IntPtr.Zero;
            WNDCLASSEXW.lpszClassName = Marshal.StringToHGlobalUni(ClassName);
            WNDCLASSEXW.hIconSm = IntPtr.Zero;

            return WNDCLASSEXW;
        }
        private IntPtr RegisterClass()
        {
            return User32.RegisterClassExW(WNDCLASSEXW);
        }
    }
}