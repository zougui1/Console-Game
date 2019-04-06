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
        public Shop(Citizen[] citizens, bool isLocked, string category) : base(citizens, isLocked, category)
        { }
    }
}
