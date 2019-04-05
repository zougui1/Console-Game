using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.game;
using ConsoleGame.misc.coords;

namespace ConsoleGame.misc.map
{
    public class Zone
    {
        public int Id { get; private set; }
        public Coords Coords { get; private set; }
        public Rect Rect { get; private set; }
        public List<Spawning> Spawnings { get; private set; }

        public Zone() { }

        [JsonConstructor]
        public Zone(Rect rect, Coords coords, int id, List<Spawning> spawnings)
        {
            Rect = rect;
            Coords = coords;
            Id = id;
            Spawnings = spawnings.OrderBy(spawning => spawning.Percent).ToList();
        }

        public void TriggerRightMonster()
        {
            double percent = 0;
            double random = RandomNumber.Between(0, 100) + new Random().NextDouble();

            for(int i = 0; i < Spawnings.Count; ++i)
            {
                Spawning spawning = Spawnings[i];

                if(random <= (percent += spawning.Percent))
                {
                    GameMenu.Game.TriggerMonster(spawning.MonsterId);
                    return;
                }
            }
        }
    }
}
