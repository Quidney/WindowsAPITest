using System.Runtime.InteropServices;

namespace QW32Lib.DataTypes.Helper
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int x;
        public int y;
    }
}