using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.entity.NPC;

namespace ConsoleGame.building
{
    public class Church : Building
    {
        public Priest Priest { get; private set; }

        [JsonConstructor]
        public Church(List<AbstractNPC> npcs, bool isLocked, string category, Priest priest) : base(npcs, isLocked, category)
        {
            Priest = priest;
        }
    }
}