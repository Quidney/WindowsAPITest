using System.Runtime.InteropServices;

namespace QW32Lib.DataTypes.Delegates
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
}