using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.cConsole
{
    public partial class CConsole
    {
        #region Writes
        public CConsole Write(float value)
        {
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(int value)
        {
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(long value)
        {
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(object value)
        {
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(string value)
        {
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(string format, object arg0)
        {
            Message.AppendFormat(format, arg0);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(string format, object arg0, object arg1, object arg2)
        {
            Message.AppendFormat(format, arg0, arg1, arg2);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Message.AppendFormat(format, arg0, arg1, arg2, arg3);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(string format, params object[] args)
        {
            Message.AppendFormat(format, args);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(decimal value)
        {
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(char[] buffer)
        {
            Message.Append(buffer);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(char value)
        {
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(bool value)
        {
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(string format, object arg0, object arg1)
        {
            Message.AppendFormat(format, arg0, arg1);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole Write(double value)
        {
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        #endregion
    }
}
