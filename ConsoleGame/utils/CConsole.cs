using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.cConsole;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        /// <summary>
        /// Cconsole property is an object to send colored messages in the console
        /// </summary>
        static public CConsole Cconsole { get; } = new CConsole();
    }
}
