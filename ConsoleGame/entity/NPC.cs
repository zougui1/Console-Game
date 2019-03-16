using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.entity
{
    public class NPC
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }

        public NPC()
        {

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
