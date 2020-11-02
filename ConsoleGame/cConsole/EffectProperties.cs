using System;
using System.Diagnostics;
using System.Text;

namespace ConsoleGame.cConsole
{
    public partial class CConsole
    {
        public CConsole Reset
        {
            get { return Effect("Reset"); }
        }

        public CConsole Bold
        {
            get { return Effect("Bold"); }
        }

        public CConsole Faint
        {
            get { return Effect("Faint"); }
        }

        public CConsole Italic
        {
            get { return Effect("Italic"); }
        }

        public CConsole Underline
        {
            get { return Effect("Underline"); }
        }

        public CConsole ReverseVideo
        {
            get { return Effect("ReverseVideo"); }
        }
    }
}
