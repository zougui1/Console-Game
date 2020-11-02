using System;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;

namespace ConsoleGame.cConsole
{
    public partial class CConsole
    {
        const int STD_OUTPUT_HANDLE = -11;
        const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 4;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

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
        private StringBuilder EffectStr { get; set; } = new StringBuilder();

        public CConsole()
        {
            var handle = GetStdHandle(STD_OUTPUT_HANDLE);
            uint mode;
            GetConsoleMode(handle, out mode);
            mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            SetConsoleMode(handle, mode);
        }
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
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public CConsole Rgb(int r, int g, int b)
        {
            Message.Append(Escape($"38;2;{r};{g};{b}"));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public CConsole BgRgb(int r, int g, int b)
        {
            Message.Append(Escape($"48;2;{r};{g};{b}"));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public CConsole Rgb(string color)
        {
            if(color == null)
            {
                return this;
            }

            string[] rgb = color.Split('(')[1].Split(')')[0].Split(',');
            Rgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
            return this;
        }

        public CConsole BgRgb(string color)
        {
            if (color == null)
            {
                return this;
            }

            string[] rgb = color.Split('(')[1].Split(')')[0].Split(',');
            BgRgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
            return this;
        }

        public CConsole Hex(string hex)
        {
            if (hex == null)
            {
                return this;
            }

            (int r, int g, int b) = HexToRgb(hex);

            Message.Append(Escape($"38;2;{r};{g};{b}"));
            return this;
        }

        public CConsole BgHex(string hex)
        {
            if (hex == null)
            {
                return this;
            }

            (int r, int g, int b) = HexToRgb(hex);

            Message.Append(Escape($"48;2;{r};{g};{b}"));
            return this;
        }

        public (int r, int g, int b) HexToRgb(string hex)
        {
            string hexCode = hex.Split('#')[1];
            int r;
            int g;
            int b;

            if (hexCode.Length == 3)
            {
                string hR = $"{hexCode[0]}{hexCode[0]}";
                r = Convert.ToInt32(hR, 16);

                string hG = $"{hexCode[1]}{hexCode[1]}";
                g = Convert.ToInt32(hG, 16);

                string hB = $"{hexCode[2]}{hexCode[2]}";
                b = Convert.ToInt32(hB, 16);
            }
            else
            {
                string hR = $"{hexCode[0]}{hexCode[1]}";
                r = Convert.ToInt32(hR, 16);

                string hG = $"{hexCode[2]}{hexCode[3]}";
                g = Convert.ToInt32(hG, 16);

                string hB = $"{hexCode[4]}{hexCode[5]}";
                b = Convert.ToInt32(hB, 16);
            }

            return (r, g, b);
        }

        private string Escape(string effect)
        {
            return $"\x1B[{effect}m";
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
            CharOffsetProp = offset;
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

        public CConsole NewLine(int amount = 1)
        {
            for(int i = 1; i < amount; i++)
            {
                Message.AppendLine();
            }

            WriteLine();
            return this;
        }

        /// <summary>
        /// Color is used to change the text color
        /// </summary>
        /// <param name="color">color name, shall beggin with an uppercase</param>
        public CConsole Color(string color)
        {
            if (color == null)
            {
                return this;
            }

            if (color[0] == '#')
            {
                Hex(color);
            }
            else if(color.Substring(0, 3) == "rgb")
            {
                Rgb(color);
            }
            else
            {
                Message.Append($"{Escape(GetColor(color).ToString())}");
            }
            EffectStr.Append(Escape(GetColor(color).ToString()));
            return this;
        }

        /// <summary>
        /// Effect is used to change the text effect
        /// </summary>
        /// <param name="color">color name, shall beggin with an uppercase</param>
        public CConsole Effect(string effect)
        {
            Message.Append($"{Escape(GetEffect(effect).ToString())}");
            EffectStr.Append(Escape(GetColor(effect).ToString()));
            return this;
        }

        /// <summary>
        /// Color is used to change the background color
        /// </summary>
        /// <param name="color">color name, shall beggin with an uppercase</param>
        public CConsole Bg(string color)
        {
            if (color == null)
            {
                return this;
            }

            if (color[0] == '#')
            {
                BgHex(color);
            }
            else if (color.Substring(0, 3) == "rgb")
            {
                BgRgb(color);
            }
            else
            {
                Message.Append($"{Escape(GetColor($"Bg{color}").ToString())}");
            }
            EffectStr.Append(Escape(GetColor($"Bg{color}").ToString()));
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

        private int GetColor(string color)
        {
            if (color == "Grey")
            {
                color = "Gray";
            }
            else if (color == "DarkGrey")
            {
                color = "DarkGray";
            }

            switch (color)
            {
                case "Reset":
                    return Colors.Reset;

                case "Black":
                    return Colors.Black;
                case "DarkRed":
                    return Colors.DarkRed;
                case "DarkGreen":
                    return Colors.DarkGreen;
                case "DarkYellow":
                    return Colors.DarkYellow;
                case "DarkBlue":
                    return Colors.DarkBlue;
                case "DarkMagenta":
                    return Colors.DarkMagenta;
                case "DarkCyan":
                    return Colors.DarkCyan;
                case "Gray":
                    return Colors.Gray;

                case "BgBlack":
                    return Colors.BgBlack;
                case "BgDarkRed":
                    return Colors.BgDarkRed;
                case "BgDarkGreen":
                    return Colors.BgDarkGreen;
                case "BgDarkYellow":
                    return Colors.BgDarkYellow;
                case "BgDarkBlue":
                    return Colors.BgDarkBlue;
                case "BgDarkMagenta":
                    return Colors.BgDarkMagenta;
                case "BgDarkCyan":
                    return Colors.BgDarkCyan;
                case "BgGray":
                    return Colors.BgGray;

                case "DarkGray":
                    return Colors.DarkGray;
                case "Red":
                    return Colors.Red;
                case "Green":
                    return Colors.Green;
                case "Yellow":
                    return Colors.Yellow;
                case "Blue":
                    return Colors.Blue;
                case "Magenta":
                    return Colors.Magenta;
                case "Cyan":
                    return Colors.Cyan;
                case "White":
                    return Colors.White;

                case "BgDarkGray":
                    return Colors.BgDarkGray;
                case "BgRed":
                    return Colors.BgRed;
                case "BgGreen":
                    return Colors.BgGreen;
                case "BgYellow":
                    return Colors.BgYellow;
                case "BgBlue":
                    return Colors.BgBlue;
                case "BgMagenta":
                    return Colors.BgMagenta;
                case "BgCyan":
                    return Colors.BgCyan;
                case "BgWhite":
                    return Colors.BgWhite;

                default:
                    return Colors.White;
            }
        }

        private int GetEffect(string effect)
        {
            switch (effect)
            {
                case "Reset":
                    return Effects.Reset;

                case "Bold":
                    return Effects.Bold;
                case "Faint":
                    return Effects.Faint;
                case "Italic":
                    return Effects.Italic;
                case "Underline":
                    return Effects.Underline;
                case "ReverseVideo":
                    return Effects.ReverseVideo;

                default:
                    return Effects.Faint;
            }
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
                            Console.Write("{0} ", EffectStr);
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
            Color("Reset");

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
