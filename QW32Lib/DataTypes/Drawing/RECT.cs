using System.Runtime.InteropServices;

namespace QW32Lib.DataTypes.Drawing
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}