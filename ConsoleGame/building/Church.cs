using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.NPC;

namespace ConsoleGame.building
{
    public class Church : Building
    {
        public Priest Priest { get; set; }

        public Church() : base()
        { }
    }
}