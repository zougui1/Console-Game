using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleGame.misc.map
{
    public struct Spawning
    {
        public int MonsterId { get; private set; }
        public double Percent { get; private set; }

        [JsonConstructor]
        public Spawning(int monsterId, double percent)
        {
            MonsterId = monsterId;
            Percent = percent;
        }
    }
}
