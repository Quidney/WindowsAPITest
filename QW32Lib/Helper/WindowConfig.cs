namespace QW32Lib.Helper
{
    public struct WindowConfig(
        uint _dwExStyle, 
        string _lpClassName,
        string _lpWindowName,
        uint _dwStyle,
        int _X,
        int _Y,
        int _nWidth,
        int _nHeight,
        IntPtr _hWndParent,
        IntPtr _hMenu,
        IntPtr _hInstance,
        IntPtr _lpParam
        )
    {
        public uint dwExStyle = _dwExStyle;
        public string lpClassName = _lpClassName;
        public string lpWindowName = _lpWindowName;
        public uint dwStyle = _dwStyle;
        public int X = _X;
        public int Y = _Y;
        public int nWidth = _nWidth;
        public int nHeight = _nHeight;
        public IntPtr hWndParent = _hWndParent;
        public IntPtr hMenu = _hMenu;
        public IntPtr hInstance = _hInstance;
        public IntPtr lpParam = _lpParam;
    }
}
