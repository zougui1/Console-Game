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
        public void WriteLine(float value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public void WriteLine(int value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public void WriteLine(uint value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public void WriteLine(long value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public void WriteLine(ulong value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public void WriteLine(object value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public void WriteLine(string format, object arg0)
        {
            Console.WriteLine(format, arg0);
            Console.ResetColor();
        }
        public void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Console.WriteLine(format, arg0, arg1, arg2);
            Console.ResetColor();
        }
        public void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Console.WriteLine(format, arg0, arg1, arg2, arg3);
            Console.ResetColor();
        }
        public void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
            Console.ResetColor();
        }
        public void WriteLine(char[] value, int index, int count)
        {
            Console.WriteLine(value, index, count);
            Console.ResetColor();
        }
        public void WriteLine(decimal value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public void WriteLine(char[] buffer)
        {
            Console.WriteLine(buffer);
            Console.ResetColor();
        }
        public void WriteLine(char value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public void WriteLine(bool value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public void WriteLine(string format, object arg0, object arg1)
        {
            Console.WriteLine(format, arg0, arg1);
            Console.ResetColor();
        }
        public void WriteLine(double value)
        {
            Console.WriteLine(value);
            Console.ResetColor();
        }
        #endregion

        #region Writes
        public void Write(float value)
        {
            Console.Write(value);
            Console.ResetColor();
        }
        public void Write(int value)
        {
            Console.Write(value);
            Console.ResetColor();
        }
        public void Write(object value)
        {
            Console.Write(value);
            Console.ResetColor();
        }
        public void Write(string value)
        {
            Console.Write(value);
            Console.ResetColor();
        }
        public void Write(string format, object arg0)
        {
            Console.Write(format, arg0);
            Console.ResetColor();
        }
        public void Write(string format, object arg0, object arg1, object arg2)
        {
            Console.Write(format, arg0, arg1, arg2);
            Console.ResetColor();
        }
        public void Write(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Console.Write(format, arg0, arg1, arg2, arg3);
            Console.ResetColor();
        }
        public void Write(string format, params object[] arg)
        {
            Console.Write(format, arg);
            Console.ResetColor();
        }
        public void Write(decimal value)
        {
            Console.Write(value);
            Console.ResetColor();
        }
        public void Write(char[] buffer)
        {
            Console.WriteLine(buffer);
            Console.ResetColor();
        }
        public void Write(char value)
        {
            Console.Write(value);
            Console.ResetColor();
        }
        public void Write(bool value)
        {
            Console.Write(value);
            Console.ResetColor();
        }
        public void Write(string format, object arg0, object arg1)
        {
            Console.Write(format, arg0, arg1);
            Console.ResetColor();
        }
        public void Write(double value)
        {
            Console.Write(value);
            Console.ResetColor();
        }
        #endregion
    }
}
