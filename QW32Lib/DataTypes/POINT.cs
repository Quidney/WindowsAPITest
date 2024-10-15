using System.Runtime.InteropServices;

namespace QW32Lib.DataTypes
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int x;
        public int y;
    }
}