using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using ConsoleGame.json;
using ConsoleGame.location;

namespace ConsoleGame
{
    public delegate void Action(object[] args);

    class Program
    {
        //[DllImport("User32.dll", EntryPoint = "MessageBox")]
        //static extern int MessageDialog(int hWnd, string msg, string caption, int msgType);

        [STAThread]
        static void Main(string[] args)
        {
            //MessageDialog(0, "MessageDialog Called", "DllImport test", 0);
            Json.Init();
            LocationList.SetLocations();
            GameStatement.Init();
            Console.ReadKey();
        }
    }
}
