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
        public static string GetLastNamespace(string fullNamespace)
        {
            string[] namespaceParts = fullNamespace.Split('.');
            return namespaceParts[namespaceParts.Length - 1];
        }
    }
}
