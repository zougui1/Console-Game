using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.misc;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static Object Caller(string className, string methodName, object[] parameters = null)
        {
            Type type = Type.GetType(className);
            MethodInfo methodInfo = type.GetMethod(methodName);
            object obj = Activator.CreateInstance(type);
            methodInfo.Invoke(obj, parameters);

            return obj;
        }

        public static object Caller(object obj, string methodName, object[] parameters = null)
        {
            Type type = obj.GetType();
            MethodInfo methodInfo = type.GetMethod(methodName);
            if (methodInfo == null)
            {
                Endl();
                Cconsole.Color("DarkRed").WriteLine("This command doesn't exists \"{0}\"", methodName);
                Endl();
                MethodInfo help = type.GetMethod("Help");
                help.Invoke(obj, null);
            }
            else
            {
                methodInfo.Invoke(obj, parameters);
            }

            return obj;
        }
    }
}
