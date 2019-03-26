using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class CConsole
    {
        private string[] m_colorNames =
        {
            "Black",
            "DarkBlue",
            "DarkGreen",
            "DarkCyan",
            "DarkRed",
            "DarkMagenta",
            "DarkYellow",
            "Gray",
            "DarkGray",
            "Blue",
            "Green",
            "Cyan",
            "Red",
            "Magenta",
            "Yellow",
            "White"
        };
        private int[] m_colors =
        {
            0,
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,
            13,
            14,
            15
        };

        public CConsole Color(string color)
        {
            Console.ForegroundColor = (ConsoleColor)GetColor(color);
            return this;
        }

        public CConsole Bg(string color)
        {
            Console.BackgroundColor = (ConsoleColor)GetColor(color);
            return this;
        }

        public int GetColor(string color)
        {
            int i;
            for(i = 0; i < m_colorNames.Length; ++i)
            {
                if(m_colorNames[i] == color)
                {
                    return m_colors[i];
                }
            }

            return 15;
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
