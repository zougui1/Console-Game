using System;
using System.Reflection;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        /// <summary>
        /// Caller call a class's method dynamically without using an existant object
        /// </summary>
        /// <param name="className">a string which contain the class name</param>
        /// <param name="methodName">a string which contain the method name</param>
        /// <param name="parameters">an array which will be used to pass it as arguments to the method</param>
        /// <returns>return what the targeted method return</returns>
        public static object Caller(string className, string methodName, object[] parameters = null)
        {
            Type type = Type.GetType(className);
            MethodInfo methodInfo = type.GetMethod(methodName);
            object obj = Activator.CreateInstance(type);
            methodInfo.Invoke(obj, parameters);

            return obj;
        }

        /// <summary>
        /// Caller call a class's method dynamically using an existant object
        /// </summary>
        /// <param name="obj">the object that contain the wanted method</param>
        /// <param name="methodName">a string which contain the method name</param>
        /// <param name="parameters">an array which will be used to pass it as arguments to the method</param>
        /// <returns>return what the targeted method return</returns>
        public static object Caller(object obj, string methodName, object[] parameters = null)
        {
            Type type = obj.GetType();
            MethodInfo methodInfo = type.GetMethod(methodName);
            methodInfo.Invoke(obj, parameters);

            return obj;
        }
    }
}
