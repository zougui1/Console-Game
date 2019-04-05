using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.building;
using ConsoleGame.entity.NPC;
using ConsoleGame.location;
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
            Menu<Action, object> menu = new Menu<Action, object>("What do you want to do?")
                .AddChoice("Move", new TAction<object>(user.ChooseDirection))
                .AddChoice("Rest", new TAction<object>(user.Rest))
                .AddChoice("Inventory", new TAction<object>(user.Inventory.Display));
            menu.Choose();
            user.ChooseAction();
        }

        public static void InLocation(User user)
        {
            Location location = GameMenu.Game.CurrentLocation;
            int buildingCount = location.Buildings.Length +
                (location.ArmorShop == null ? 0 : 1) +
                (location.Church == null ? 0 : 1) +
                (location.ItemShop == null ? 0 : 1) +
                (location.WeaponShop == null ? 0 : 1);

            Menu<Action, User> menu = new Menu<Action, User>("What do you want to do?")
                .AddChoice($"Talk to a citizen ({location.Citizens.Length})", new TAction<User>(ChooseCitizen), user)
                .AddChoice($"Enter in a building ({buildingCount})", new TAction<User>(ChooseBuilding), user)
                .AddChoice($"Quit \"{location.Name}\"", new TAction<User>(User => { GameMenu.Game.Statement = GameStatement.Wilderness; }));
            Utils.Endl();
            menu.Choose();
            user.ChooseAction();
        }

        public static void ChooseCitizen(User user)
        {
            Location location = GameMenu.Game.CurrentLocation;
            Menu<Action, object> menu = new Menu<Action, object>("With which citizen do you want to talk?");

            for(int i = 0; i < location.Citizens.Length; i++)
            {
                Citizen citizen = location.Citizens[i];

                menu.AddChoice($"Talk to {citizen.Name} ({citizen.Category})", new TAction<object>(citizen.Discussion));
            }
            menu.AddChoice("none", new TAction<object>(BackToInLocation));

            Utils.Endl();
            menu.Choose();
        }

        public static void ChooseBuilding(User user)
        {
            Location location = GameMenu.Game.CurrentLocation;
            Menu<Action, object> menu = new Menu<Action, object>("Which building do you want to enter in?");

            for (int i = 0; i < location.Buildings.Length; i++)
            {
                Building building = location.Buildings[i];

                menu.AddChoice($"Enter into {building.Category}", new TAction<object>(building.Enter));
            }
            menu.AddChoice("none", new TAction<object>(BackToInLocation));

            Utils.Endl();
            menu.Choose();
        }

        public static void BackToInLocation(object arg = null)
        {
            GameMenu.Game.Statement = GameStatement.InLocation;
        }
    }
}
