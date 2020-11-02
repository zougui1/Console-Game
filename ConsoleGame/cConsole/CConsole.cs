using System;
using System.Diagnostics;
using System.Text;

namespace ConsoleGame.cConsole
{
    public partial class CConsole
    {
        /// <summary>
        /// LeftProp determine wether or no the text should be written at left
        /// </summary>
        private bool LeftProp { get; set; } = false;
        /// <summary>
        /// RightProp determine wether or no the text should be written at right
        /// </summary>
        private bool RightProp { get; set; } = false;
        private bool TimerProp { get; set; } = false;
        private Stopwatch Stopwatch { get; set; } = new Stopwatch();
        /// <summary>
        /// CharOffsetProp determine how many space to get at the right position
        /// get overriden by OffsetProp
        /// </summary>
        private int CharOffsetProp { get; set; } = 0;
        /// <summary>
        /// OffsetProp determine the position of the cursor on the horizontal abscissa
        /// override CharOffset
        /// </summary>
        private int OffsetProp { get; set; } = -1;
        /// <summary>
        /// Line determine wether or no the text is a full line
        /// </summary>
        private bool Line { get; set; } = false;
        /// <summary>
        /// AbsoluteProp determine wether or no the position is absolute
        /// </summary>
        private bool AbsoluteProp { get; set; } = false;
        private bool MultiProp { get; set; } = false;
        private int InitialTop { get; set; }
        private int TopProp { get; set; } = -1;
        private StringBuilder Message { get; set; } = new StringBuilder();

        public CConsole() { }
        private CConsole(bool multi)
        {
            MultiProp = true;
        }

        private void ResetProperties()
        {
            Message.Clear();
            Line = false;

            if (!MultiProp)
            {
                Console.ResetColor();
                LeftProp = false;
                RightProp = false;
                CharOffsetProp = 0;
                AbsoluteProp = false;
                OffsetProp = -1;

                if (TopProp >= 0)
                {
                    Console.SetCursorPosition(Console.CursorLeft, InitialTop);
                }

                InitialTop = 0;
                TopProp = -1;
            }
        }

        /// <summary>
        /// Absolute is used to set the text in an absolute position, from the beginning of the line
        /// with a CharOffset it'll overwrite the line
        /// with an OffsetProp it won't overwrite the line
        /// </summary>
        public CConsole Absolute()
        {
            AbsoluteProp = true;
            return this;
        }

        /// <summary>
        /// Offset is used not to overwrite the line if the position is absolute
        /// </summary>
        public CConsole Offset(int offset)
        {
            OffsetProp = offset;
            return this;
        }

        /// <summary>
        /// CharOffset is used to shift the position, the shift direction will depend of the text direction
        /// </summary>
        public CConsole CharOffset(int offset)
        {
            OffsetProp = offset;
            return this;
        }

        /// <summary>
        /// Top is used to define the cursor top position
        /// </summary>
        /// <param name="offset">cursor top position</param>
        public CConsole Top(int position)
        {
            InitialTop = Console.CursorTop;
            TopProp = position;
            return this;
        }

        /// <summary>
        /// Left is used to place the text at left
        /// </summary>
        /// <param name="offset">offset is used to shift the position from the left</param>
        public CConsole Left(int offset = 0)
        {
            LeftProp = true;
            RightProp = false;
            CharOffsetProp = offset;
            return this;
        }

        /// <summary>
        /// Right is used to place the text at right
        /// </summary>
        /// <param name="offset">offset is used to shift the position from the right</param>
        public CConsole Right(int offset = 0)
        {
            RightProp = true;
            LeftProp = false;
            CharOffsetProp = offset;
            return this;
        }

        /// <summary>
        /// Color is used to change the text color
        /// </summary>
        /// <param name="color">color name, shall beggin with an uppercase</param>
        public CConsole Color(string color)
        {
            Console.ForegroundColor = GetColor(color);
            return this;
        }

        /// <summary>
        /// Color is used to change the background color
        /// </summary>
        /// <param name="color">color name, shall beggin with an uppercase</param>
        public CConsole Bg(string color)
        {
            Console.BackgroundColor = GetColor(color);
            return this;
        }

        public CConsole TimerStart()
        {
            Stopwatch.Start();
            return this;
        }

        public CConsole TimerStop()
        {
            Stopwatch.Stop();
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            Absolute().Right().Top(Console.WindowHeight - 1).Color("Cyan").Write("{0}", Stopwatch.Elapsed);
            Stopwatch.Reset();

            Console.CursorTop = 0;
            Console.SetCursorPosition(cursorLeft, cursorTop);
            return this;
        }

        /// <summary>
        /// Multi is used to call one or more time Write and/or WriteLine with the same properties without having to rewrite them
        /// </summary>
        /// <param name="multiCall">receive a CConsole object as argument</param>
        public CConsole Multi(Func<CConsole, CConsole> multiCall)
        {
            InitialTop = Console.CursorTop;
            multiCall(new CConsole(true));
            return this;
        }

        private ConsoleColor GetColor(string color)
        {
            if (color == "Grey")
            {
                color = "Gray";
            }
            else if (color == "DarkGrey")
            {
                color = "DarkGray";
            }

            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }

        private void Writer()
        {
            Positionate();

            int offset = CharOffsetProp;

            if (LeftProp || RightProp)
            {
                if (RightProp)
                {
                    offset = -offset;
                    offset += Console.WindowWidth - Message.Length;
                }

                for (int i = 0; i < Console.WindowWidth; ++i)
                {
                    if (i == offset)
                    {
                        Write();
                    }
                    else if (i < offset || i > (offset + Message.Length + CharOffsetProp))
                    {
                        if (!AbsoluteProp || OffsetProp < 0)
                        {
                            Console.Write(' ');
                        }
                    }
                }
            }
            else
            {
                Write();
            }
        }

        private void Positionate()
        {
            int offsetLeft = OffsetProp > 0 ? OffsetProp : 0;
            int offsetTop = TopProp >= 0 ? TopProp : Console.CursorTop;

            if (AbsoluteProp)
            {
                if (RightProp && OffsetProp >= 0)
                {
                    Console.SetCursorPosition(Console.WindowWidth - (offsetLeft + Message.Length), offsetTop);
                }
                else
                {
                    Console.SetCursorPosition(offsetLeft, offsetTop);
                }
            }
        }

        private void Write()
        {
            if (Line)
            {
                Console.WriteLine(Message);
            }
            else
            {
                Console.Write(Message);
            }
        }
    }
}

/* examples of use
 * CConsole Cconsole = new CConsole();
 * // basic positions (left by default)
 * Cconsole.Color("Cyan").WriteLine("some text");
 * Cconsole.Right().Color("Yellow").Write("right text");
 * 
 * Utils.FillLine('-');
 * 
 * // by default positions are relatives
 * Cconsole.Color("DarkRed").Write("left relative text");
 * Cconsole.Right().Color("Red").WriteLine("right relative text");
 * 
 * Utils.FillLine('-');
 * 
 * // position as absolute by default overwrite the line
 * Cconsole.Color("DarkRed").Write("i'm invisible");
 * Cconsole.Right().Absolute().Color("Red").Write("i overwritten the line");
 * 
 * Utils.FillLine('-');
 * 
 * // Offset can be used to not overwrite the line
 * Cconsole.Color("DarkRed").Write("i'm visible");
 * Cconsole.Right().Absolute().Offset(0).Color("Red").Write("i didn't overwrite the line");
 * 
 * Utils.FillLine('-');
 * 
 * // Right can take an offset as argument, or use Offset as well, not cumullable
 * // Offset cannot be used for non-absolute text
 * Cconsole.Right(10).Color("Red").WriteLine("not absolute and right-offset of 10");
 * Cconsole.Right(10).Absolute().Offset(10).Color("Red").WriteLine("absolute and offset of 10 for both methods");
 * Cconsole.Right().Offset(10).Color("Red").WriteLine("absolute and offset of 10 for both methods");
 * 
 * Utils.FillLine('-');
 * 
 * // the properties are reset at each call of Write and WriteLine, Multi can be used to bypass it
 * Cconsole.Right().Color("Red").Write("write call: 1").WriteLine("write call: 2");
 * Cconsole.Multi(console => console.Right().Color("Blue").Write("multi, write call: 1").Write("multi, write call: 2"));
 */
