using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.NPC;
using ConsoleGame.game;

namespace ConsoleGame.building
{
    public class Building
    {
        public List<AbstractNPC> NPCs { get; private set; }
        public bool IsLocked { get; private set; }
        public string Category { get; private set; }
        
        public Building(List<AbstractNPC> npcs, bool isLocked, string category)
        {
            NPCs = npcs;
            IsLocked = isLocked;
            Category = category;
        }

        public virtual void Enter(object arg = null)
        {
            GameMenu.Game.Statement = GameStatement.InBuilding;
            GameMenu.Game.CurrentBuilding.SetCurrentBuilding(this);
        }
    }
}
