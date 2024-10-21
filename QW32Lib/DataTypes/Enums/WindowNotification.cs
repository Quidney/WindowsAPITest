namespace QW32Lib.Enums
{
    internal enum WindowNotification : uint
    {
        WM_ACTIVATEAPP = 0x001C,              // 0x001C
        WM_CANCELMODE = 0x001F,               // 0x001F
        WM_CHILDACTIVATE = 0x0021,            // 0x0021
        WM_CLOSE = 0x0010,                    // 0x0010
        WM_COMPACTING = 0x0011,               // 0x0011
        WM_CREATE = 0x0001,                   // 0x0001
        WM_DESTROY = 0x0002,                  // 0x0002
        WM_DPICHANGED = 0x02E0,               // 0x02E0
        WM_ENABLE = 0x000A,                   // 0x000A
        WM_ENTERSIZEMOVE = 0x0231,            // 0x0231
        WM_EXITSIZEMOVE = 0x0232,             // 0x0232
        WM_GETICON = 0x007F,                  // 0x007F
        WM_GETMINMAXINFO = 0x0024,            // 0x0024
        WM_INPUTLANGCHANGE = 0x0051,          // 0x0051
        WM_INPUTLANGCHANGEREQUEST = 0x0050,   // 0x0050
        WM_MOVE = 0x0003,                     // 0x0003
        WM_MOVING = 0x0216,                   // 0x0216
        WM_NCACTIVATE = 0x0086,               // 0x0086
        WM_NCCALCSIZE = 0x0083,               // 0x0083
        WM_NCCREATE = 0x0081,                 // 0x0081
        WM_NCDESTROY = 0x0082,                // 0x0082
        WM_NULL = 0x0000,                     // 0x0000
        WM_QUERYDRAGICON = 0x0037,            // 0x0037
        WM_QUERYOPEN = 0x0013,                // 0x0013
        WM_QUIT = 0x0012,                     // 0x0012
        WM_SHOWWINDOW = 0x0018,               // 0x0018
        WM_SIZE = 0x0005,                     // 0x0005
        WM_SIZING = 0x0214,                   // 0x0214
        WM_STYLECHANGED = 0x007D,             // 0x007D
        WM_STYLECHANGING = 0x007C,            // 0x007C
        WM_THEMECHANGED = 0x031A,             // 0x031A
        WM_USERCHANGED = 0x0040,              // 0x0040
        WM_WINDOWPOSCHANGED = 0x0047,         // 0x0047
        WM_WINDOWPOSCHANGING = 0x0046,        // 0x0046

        WM_PAINT
    }
}
