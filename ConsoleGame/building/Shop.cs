
using ConsoleGame.entity.NPC;

namespace ConsoleGame.building
{
    public class Shop : Building
    {
        public Shop(Citizen[] citizens, bool isLocked, string category) : base(citizens, isLocked, category)
        { }
    }
}
