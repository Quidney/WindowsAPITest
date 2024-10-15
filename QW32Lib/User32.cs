using QW32Lib.DataTypes.Drawing;
using QW32Lib.DataTypes.Helper;
using QW32Lib.Enums;
using System.Runtime.InteropServices;

namespace QW32Lib
{
    internal static partial class User32
    {
        const string User32DLL = "user32.dll";

        [LibraryImport(User32DLL, SetLastError = true)]
        internal static partial IntPtr RegisterClassExW(WNDCLASSEXW lpwcx);

        [LibraryImport(User32DLL, SetLastError = true)]
        internal static partial IntPtr CreateWindowExW(
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
        internal static IntPtr CreateWindowExW(WindowConfig config) => CreateWindowExW(
            config.dwExStyle,
            config.lpClassName,
            config.lpWindowName,
            config.dwStyle,
            config.X,
            config.Y,
            config.nWidth,
            config.nHeight,
            config.hWndParent,
            config.hMenu,
            config.hInstance,
            config.lpParam
        );

        [LibraryImport(User32DLL, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [LibraryImport(User32DLL, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool DestroyWindow(IntPtr hWnd);


        [LibraryImport(User32DLL, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool GetMessageW(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [LibraryImport(User32DLL, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool TranslateMessage(ref MSG lpMsg);

        [LibraryImport(User32DLL, SetLastError = true)]
        internal static partial IntPtr DispatchMessageW(ref MSG lpMsg);

        [LibraryImport(User32DLL, SetLastError = true)]
        internal static partial void PostQuitMessage(int nExitCode);

        [LibraryImport(User32DLL, SetLastError = true)]
        internal static partial IntPtr DefWindowProcW(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [LibraryImport(User32DLL, SetLastError = true)]
        internal static partial IntPtr LoadCursorW(IntPtr hInstance, IntPtr lpCursorName);



        [LibraryImport(User32DLL, SetLastError = true)]
        internal static partial int GetSystemMetrics(int nIndex);
        internal static int GetSystemMetrics(SystemMetrics nIndex) => GetSystemMetrics((int)nIndex);


        [LibraryImport(User32DLL, SetLastError = true)]
        internal static partial IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);
    }
}
