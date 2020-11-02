using ConsoleGame.utils;
using Newtonsoft.Json;
using System;

namespace ConsoleGame.entity.NPC
{
    public class Citizen : AbstractNPC
    {
        public string Text { get; private set; }

        [JsonConstructor]
        public Citizen(string name, string category, string text) : base(name, category)
        {
            Text = text;
        }

        public void Discussion(object arg = null)
        {
            int timeout = 25;

            Utils.Endl();
            Utils.SetTimeout(() =>
            {
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
                timeout = 0;
            }, 0);

            for (int i = 0; i < Text.Length; i++)
            {
                Utils.SetTimeoutSync(() => Console.Write(Text[i]), timeout);
            }
            Utils.Endl();

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
