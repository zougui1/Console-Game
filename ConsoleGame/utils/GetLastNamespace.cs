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
