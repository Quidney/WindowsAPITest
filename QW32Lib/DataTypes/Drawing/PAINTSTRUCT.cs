using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace QW32Lib.DataTypes.Drawing
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PAINTSTRUCT
    {
        public IntPtr hdc;
        [MarshalAs(UnmanagedType.Bool)] public bool fErase;
        public RECT rcPaint;
        [MarshalAs(UnmanagedType.Bool)] public bool fRestore;
        [MarshalAs(UnmanagedType.Bool)] public bool fIncUpdate;
        public IntPtr rgbReserved;
    }
}