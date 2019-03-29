using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class CConsole
    {
        public CConsole Color(string color)
        {
            Console.ForegroundColor = GetColor(color);
            return this;
        }

        public CConsole Bg(string color)
        {
            Console.BackgroundColor = GetColor(color);
            return this;
        }

        public ConsoleColor GetColor(string color)
        {
            if(color == "Grey")
            {
                color = "Gray";
            }

            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }

        #region WriteLines
        public CConsole WriteLine(float value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(int value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(uint value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(long value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(ulong value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(object value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(string value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(string format, object arg0)
        {
            Console.WriteLine(format, arg0);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Console.WriteLine(format, arg0, arg1, arg2);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Console.WriteLine(format, arg0, arg1, arg2, arg3);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(char[] value, int index, int count)
        {
            Console.WriteLine(value, index, count);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(decimal value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(char[] buffer)
        {
            Console.WriteLine(buffer);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(char value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(bool value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(string format, object arg0, object arg1)
        {
            Console.WriteLine(format, arg0, arg1);
            Console.ResetColor();
            return this;
        }
        public CConsole WriteLine(double value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
            return this;
        }
        #endregion

        #region Writes
        public CConsole Write(float value)
        {
            Console.Write(value);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(int value)
        {
            Console.Write(value);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(long value)
        {
            Console.Write(value);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(object value)
        {
            Console.Write(value);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(string value)
        {
            Console.Write(value);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(string format, object arg0)
        {
            Console.Write(format, arg0);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(string format, object arg0, object arg1, object arg2)
        {
            Console.Write(format, arg0, arg1, arg2);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Console.Write(format, arg0, arg1, arg2, arg3);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(string format, params object[] arg)
        {
            Console.Write(format, arg);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(decimal value)
        {
            Console.Write(value);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(char[] buffer)
        {
            Console.WriteLine(buffer);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(char value)
        {
            Console.Write(value);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(bool value)
        {
            Console.Write(value);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(string format, object arg0, object arg1)
        {
            Console.Write(format, arg0, arg1);
            Console.ResetColor();
            return this;
        }
        public CConsole Write(double value)
        {
            Console.Write(value);
            Console.ResetColor();
            return this;
        }
        #endregion
    }
}
