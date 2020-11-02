
using ConsoleGame.utils;

namespace ConsoleGame.UI.lists
{
    public class ListItem<TItem>
    {
        /// <summary>
        /// Item represent the object to display
        /// </summary>
        public TItem Item { get; set; }
        /// <summary>
        /// CursorTop represent the top position of the text
        /// </summary>
        public int CursorTop { get; private set; }
        /// <summary>
        /// Color represent the color of the text to display
        /// </summary>
        public string EvenColor { get; set; } = "White";
        public string OddColor { get; set; } = "DarkGray";
        public string EvenBgColor { get; set; }
        public string OddBgColor { get; set; } = "#111";
        public string FocusColor { get; set; }
        public string FocusBgColor { get; set; } = "#1425ff";
        public string Text { get; set; }
        public bool IsEven { get; private set; }

        public ListItem(TItem item)
        {
            Item = item;
            Text = Item.ToString();
        }

        /// <summary>
        /// HandleFocus is used to display the text with the right method
        /// DisplayFocus if the current top position is the same as the text one
        /// DisplayText otherwise
        /// </summary>
        /// <param name="currentCursorTop">the current cursor top position</param>
        public void HandleFocus(int currentCursorTop)
        {
            if (currentCursorTop == CursorTop)
            {
                DisplayFocus();
            }
            else
            {
                DisplayText();
            }
        }

        /// <summary>
        /// Init is used to define the text color and its top position
        /// </summary>
        /// <param name="cursorPosition">the top position of the text</param>
        /// <param name="color">the color of the text</param>
        public void Init(int cursorPosition)
        {
            CursorTop = cursorPosition;
            IsEven = CursorTop % 2 == 0;
        }

        /// <summary>
        /// DisplayText is used to display the text
        /// </summary>
        public void DisplayText()
        {
            Utils.Cconsole.Bg(GetBgColor()).Color(GetColor()).WriteLine(Text);
        }

        private string GetBgColor()
        {
            return IsEven
                ? EvenBgColor
                : OddBgColor;
        }

        private string GetColor()
        {
            return IsEven
                ? EvenColor
                : OddColor;
        }

        /// <summary>
        /// DisplayFocus is used to set the background color in darkgray 
        /// </summary>
        public void DisplayFocus()
        {

            /*if (tempColor == "DarkGray")
            {
                tempColor = "White";
            }*/

            //Utils.Cconsole.BgDarkGray.Color(tempColor).WriteLine(Text);
            Utils.Cconsole.Bg(FocusBgColor).Color(FocusColor ?? GetColor()).WriteLine(Text);
        }
    }
}
