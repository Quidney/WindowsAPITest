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
            while (User32.GetMessageW(out MSG msg, IntPtr.Zero, 0, 0))
            {
                User32.TranslateMessage(ref msg);
                User32.DispatchMessageW(ref msg);
            }
        }

    }
}
