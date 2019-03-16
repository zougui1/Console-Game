using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.building
{
    public class Building
    {
        public string Name { get; set; }
        public List<dynamic> NPCs { get; set; }
        public bool IsLocked { get; set; }
        public string Type { get; set; }

        public Building()
        {

        }
    }
}
