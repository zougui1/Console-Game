using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleGame.misc;

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
