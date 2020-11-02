namespace ConsoleGame.cConsole
{
    public partial class CConsole
    {
        #region WriteLines
        public CConsole WriteLine()
        {
            Line = true;
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(float value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(int value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(uint value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(long value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(ulong value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(object value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(string value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(string format, object arg0)
        {
            Line = true;
            Message.AppendFormat(format, arg0);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Line = true;
            Message.AppendFormat(format, arg0, arg1, arg2);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Line = true;
            Message.AppendFormat(format, arg0, arg1, arg2, arg3);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(string format, params object[] args)
        {
            Line = true;
            Message.AppendFormat(format, args);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(char[] value, int index, int count)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(decimal value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(char[] buffer)
        {
            Line = true;
            Message.Append(buffer);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(char value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(bool value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(string format, object arg0, object arg1)
        {
            Line = true;
            Message.AppendFormat(format, arg0, arg1);
            Writer();
            ResetProperties();
            return this;
        }
        public CConsole WriteLine(double value)
        {
            Line = true;
            Message.Append(value);
            Writer();
            ResetProperties();
            return this;
        }
        #endregion
    }
}
