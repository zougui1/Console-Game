using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.entity.NPC;
using ConsoleGame.game;

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

        public override void Enter(object arg = null)
        {
            GameMenu.Game.Statement = GameStatement.InBuilding;
            GameMenu.Game.CurrentBuilding.SetCurrentBuilding(this);
        }
    }
}