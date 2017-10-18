using System;
using System.Runtime.InteropServices;

namespace OpenEngine
{
    public static class Debug
    {
        #region DllImports
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        #endregion
        private static bool _isCmdHidden = false;

        public static bool HideConsole()
        {
            if (_isCmdHidden) return false;

            var ptr = GetConsoleWindow();
            if (ptr != IntPtr.Zero)
            {
                ShowWindow(ptr, 0);
                _isCmdHidden = true;
                return true;
            }

            return false;
        }

        public static void Log(object obj)
        {
            if (_isCmdHidden) return;

            Console.WriteLine(obj);
        }
    }
}
