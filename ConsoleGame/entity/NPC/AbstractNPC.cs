using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.entity.NPC
{
    public class AbstractNPC
    {
        public string Name { get; private set; }
        public string Category { get; private set; }

        public AbstractNPC(string name, string category)
        {
            Name = name;
            Category = category;
        }
    }
}
