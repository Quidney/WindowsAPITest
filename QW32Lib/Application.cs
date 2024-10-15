using QW32Lib.DataTypes.Helper;

namespace QW32Lib
{
    /// <summary>
    /// Abstraction
    /// </summary>
    public static class Application
    {
        public static void Run() => MessageLoop();

        private static void MessageLoop()
        {
            MSG msg;
            while (User32.GetMessageW(out msg, IntPtr.Zero, 0, 0))
            {
                User32.TranslateMessage(ref msg);
                User32.DispatchMessageW(ref msg);
            }
        }

    }
}
