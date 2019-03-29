using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.utils;

namespace ConsoleGame.game
{
    public static class Actions
    {
        /// <summary>
        /// ChooseAction is used to let the user choose an action when in the nature
        /// (move, rest)
        /// </summary>
        public static void Wilderness(User user)
        {
            Utils.Endl();
            Utils.Cconsole.Color("DarkGray").WriteLine("What do you want to do?");

            string[] actions = { "Move", "Rest", "Inventory" };
            Action[] methods = { new Action(user.ChooseDirection), new Action(user.Rest), new Action(user.Inventory.Display) };
            Utils.Choices(actions, methods);
            user.ChooseAction();
        }
    }
}
