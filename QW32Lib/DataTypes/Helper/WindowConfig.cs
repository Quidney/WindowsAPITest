namespace QW32Lib.DataTypes.Helper
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
        nint _hWndParent,
        nint _hMenu,
        nint _hInstance,
        nint _lpParam
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
        public nint hWndParent = _hWndParent;
        public nint hMenu = _hMenu;
        public nint hInstance = _hInstance;
        public nint lpParam = _lpParam;
    }
}
