using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.utils;

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

        public void Discussion()
        {
            Utils.Endl();
            for (int i = 0; i < Text.Length; ++i)
            {
                Console.Write(Text[i]);
                Thread.Sleep(25);
            }
            Utils.Endl();
        }
    }
}
