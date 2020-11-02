using ConsoleGame.game;
using ConsoleGame.utils;
using System;

namespace ConsoleGame.UI.header
{
    public static class Header
    {
        public static int CoordsOffset { get; set; } = 1;
        public static int GoldOffset { get; set; } = 1;
        public static User User { get; } = GameMenu.Game.User;

        public static void Render()
        {
            Utils.Cconsole.Absolute().Top(0).Right().Write("") // is used to remove the previous render before rewrite it
                 .Color("Cyan").Top(0).Absolute().Offset(CoordsOffset).Write("X: {0}; Y: {1};", User.Coords.X, User.Coords.Y)
                 .Color("Yellow").Top(0).Absolute().Right().Offset(GoldOffset).Write("GP: {0}", User.Gold);

            if (Console.CursorTop == 0)
            {
                Utils.Endl(2);
            }
        }
    }
}
