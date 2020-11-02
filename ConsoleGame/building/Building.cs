
using ConsoleGame.entity.NPC;
using ConsoleGame.game;

namespace ConsoleGame.building
{
    public class Building
    {
        public Citizen[] Citizens { get; set; }
        public bool IsLocked { get; private set; }
        public string Category { get; private set; }

        public Building(Citizen[] citizens, bool isLocked, string category)
        {
            Citizens = citizens;
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
