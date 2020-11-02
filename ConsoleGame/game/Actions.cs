using ConsoleGame.building;
using ConsoleGame.entity.NPC;
using ConsoleGame.location;
using ConsoleGame.UI.menus;
using ConsoleGame.utils;
using System;

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
            Menu<Action, object> menu = new Menu<Action, object>("What do you want to do?")
                .AddChoice("Move", new TAction<object>(user.ChooseDirection))
                .AddChoice("Rest", new TAction<object>(user.Rest))
                .AddChoice("Inventory", new TAction<object>(ChooseInventory));
            menu.Kind = "UI";
            menu.SinglePage = true;
            menu.InitSelection();
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
                .AddChoice($"Exit \"{location.Name}\"", new TAction<User>(User =>
                {
                    GameMenu.Game.Statement = GameStatement.Wilderness;
                    user.Coords.MoveDown();
                }));
            Utils.Endl();
            menu.Kind = "UI";
            menu.InitSelection();
            user.ChooseAction();
        }

        public static void InBuilding(User user)
        {
            Building building = GameMenu.Game.CurrentBuilding.GetRightBuilding();
            Menu<Action, object> menu = new Menu<Action, object>("What do you want to do?");

            if (building.Category == "WeaponShop")
            {
                menu.AddChoice("Talk to the weapon merchant", ((WeaponShop)building).DisplayList);
            }
            else if (building.Category == "ArmorShop")
            {
                menu.AddChoice("Talk to the armor merchant", ((ArmorShop)building).DisplayList);
            }
            else if (building.Category == "ItemShop")
            {
                menu.AddChoice("Talk to the item merchant", ((ItemShop)building).DisplayList);
            }
            else if (building.Category == "Church")
            {
                menu.AddChoice("Talk to the priest", ((Church)building).PriestInteraction);
            }

            if (building.Citizens != null)
            {
                for (int i = 0; i < building.Citizens.Length; i++)
                {
                    Citizen citizen = building.Citizens[i];
                    menu.AddChoice($"Talk to {citizen.Name} ({citizen.Category})", new TAction<object>(citizen.Discussion));
                }
            }

            menu.AddChoice("Exit", new TAction<object>(BackToInLocation));

            Utils.Endl();
            menu.Kind = "UI";
            menu.InitSelection();
            user.ChooseAction();
        }

        public static void ChooseCitizen(User user)
        {
            Location location = GameMenu.Game.CurrentLocation;
            Menu<Action, object> menu = new Menu<Action, object>("With which citizen do you want to talk?");

            for (int i = 0; i < location.Citizens.Length; i++)
            {
                Citizen citizen = location.Citizens[i];

                menu.AddChoice($"Talk to {citizen.Name} ({citizen.Category})", new TAction<object>(citizen.Discussion));
            }
            menu.AddChoice("none", new TAction<object>(BackToInLocation));

            Utils.Endl();
            menu.Kind = "UI";
            menu.InitSelection();
        }

        public static void ChooseBuilding(User user)
        {
            Location location = GameMenu.Game.CurrentLocation;
            Menu<Action, object> menu = new Menu<Action, object>("Which building do you want to enter in?");

            if (location.WeaponShop != null)
            {
                menu.AddChoice("Enter into the weapon shop", new TAction<object>(location.WeaponShop.Enter));
            }
            if (location.ArmorShop != null)
            {
                menu.AddChoice("Enter into the armor shop", new TAction<object>(location.ArmorShop.Enter));
            }
            if (location.ItemShop != null)
            {
                menu.AddChoice("Enter into the item shop", new TAction<object>(location.ItemShop.Enter));
            }
            if (location.Church != null)
            {
                menu.AddChoice("Enter into the church", new TAction<object>(location.Church.Enter));
            }

            for (int i = 0; i < location.Buildings.Length; i++)
            {
                Building building = location.Buildings[i];

                menu.AddChoice($"Enter into {building.Category}", new TAction<object>(building.Enter));
            }
            menu.AddChoice("none", new TAction<object>(BackToInLocation));

            Utils.Endl();
            menu.Kind = "UI";
            menu.InitSelection();
        }

        public static void ChooseInventory(object arg = null)
        {
            User user = GameMenu.Game.User;

            Menu<Action, object> menu = new Menu<Action, object>("Which inventory do you want to look in?");

            for(int i = 0; i < user.Characters.Count; i++)
            {
                menu.AddChoice(user.Characters[i].Name, new TAction<object>(user.Characters[i].Inventory.Display));
            }

            menu.AddChoice("Bag", user.Bag.Display);

            Utils.Endl();
            menu.Kind = "UI";
            menu.SinglePage = true;
            menu.InitSelection();
        }

        public static void BackToInLocation(object arg = null)
        {
            GameMenu.Game.Statement = GameStatement.InLocation;
        }
    }
}
