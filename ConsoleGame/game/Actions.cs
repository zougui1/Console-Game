using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.UI.menus;

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
            Menu<System.Action, object> menu = new Menu<System.Action, object>("What do you want to do?")
                .AddChoice("Move", new TAction<object>(user.ChooseDirection))
                .AddChoice("Rest", new TAction<object>(user.Rest))
                .AddChoice("Inventory", new TAction<object>(user.Inventory.Display));
            menu.Choose();
            user.ChooseAction();
        }
    }
}
