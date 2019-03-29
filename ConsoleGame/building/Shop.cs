using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.NPC;

namespace ConsoleGame.building
{
    public class Shop : Building
    {
        public Shop(List<AbstractNPC> npcs, bool isLocked, string category) : base(npcs, isLocked, category)
        { }
    }
}
