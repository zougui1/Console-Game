using ConsoleGame.game;
using ConsoleGame.json;
using ConsoleGame.UI.menus;
using ConsoleGame.utils;
using Newtonsoft.Json;
using System;

namespace ConsoleGame.entity.NPC
{
    public class Priest : AbstractNPC
    {
        [JsonConstructor]
        public Priest(string name, string category) : base(name, category)
        { }

        public void Interaction()
        {
            Menu<Action, object> menu = new Menu<Action, object>("What do you want to do?")
                .AddChoice("Confession (save)", new TAction<object>(SaveParty));

            Utils.Endl();
            menu.Choose();

            //Console.WriteLine("1. Confession (save)"); // save the party
            //Console.WriteLine("2. Resurrection"); // resurrect a member
            //Console.WriteLine("3. Benediction"); // remove curse status effect from an afflicted party member
            //Console.WriteLine("4. Purrification"); // remove poison status effect from an afflicted party member
            //Console.WriteLine("5. Divination"); // states the number of XPs the party members must gain in order to reach their next levels
        }

        private void SaveParty(object arg = null)
        {
            Json.Save(GameMenu.Game);
            Utils.Cconsole.Color("Green").WriteLine("Your party has been saved.");
            bool exitDiscussion = false;

            while (!exitDiscussion)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        exitDiscussion = true;
                        break;
                }
            }
        }
    }
}
