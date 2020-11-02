using System;
using System.Reflection;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        /// <summary>
        /// Setter is used to set a value to a property
        /// </summary>
        /// <param name="obj">The object which contain the given property</param>
        /// <param name="propertyName">the property name which we want to set the given value</param>
        /// <param name="value">the value which we want to be set to the given property</param>
        public static void Setter(object obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            propertyInfo.SetValue(obj, value);
        }

        /// <summary>
        /// Setter is used to set a value to a property
        /// </summary>
        /// <param name="obj">The object which contain the given property</param>
        /// <param name="propertyName">the property name which we want to set the given value</param>
        /// <param name="value">the value which we want to be set to the given property</param>
        public static void Setter(string objName, string propertyName, object value)
        {
            Type type = Type.GetType(objName);
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            object obj = Activator.CreateInstance(type, null);
            propertyInfo.SetValue(obj, value);
        }
    }
}
