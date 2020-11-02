namespace ConsoleGame.misc.coords
{
    public class Coords
    {
        /// <summary>
        /// X represent the coord x (left or right) if it's positive it's right otherwise left
        /// </summary>
        public int X { get; protected set; }
        /// <summary>
        /// Y represent the coord y (top or bottom) if it's positive it's bottom otherwise top
        /// </summary>
        public int Y { get; protected set; }

        public Coords(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
    }
}
