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
        public static void SetTimeout(System.Action action, int millisecondsTimeout)
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(millisecondsTimeout);
                action();
            });
        }

        public static void SetTimeoutSync(System.Action action, int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
            action();
        }
    }
}
