using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static void SetTimeout(Action action, int millisecondsTimeout = 0)
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(millisecondsTimeout);
                action();
            });
        }

        public static void SetTimeoutSync(Action action, int millisecondsTimeout = 1000)
        {
            Thread.Sleep(millisecondsTimeout);
            action();
        }
    }
}
