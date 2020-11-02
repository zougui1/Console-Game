
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
        public string Color { get; set; }

        public ListItem(TItem item)
        {
            Item = item;
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
        public void Init(int cursorPosition, string color = "White")
        {
            Color = color;
            CursorTop = cursorPosition;
        }

        /// <summary>
        /// DisplayText is used to display the text
        /// </summary>
        public void DisplayText()
        {
            Utils.Cconsole.Color(Color).Write(Item.ToString());
        }

        /// <summary>
        /// DisplayFocus is used to set the background color in darkgray 
        /// </summary>
        public void DisplayFocus()
        {
            string tempColor = Color;

            if (tempColor == "DarkGray")
            {
                tempColor = "White";
            }

            Utils.Cconsole.Bg("DarkGray").Color(tempColor).Write(Item.ToString());
        }
    }
}
