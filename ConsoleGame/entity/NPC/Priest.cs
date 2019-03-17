using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.entity.NPC
{
    public class Priest : AbstractNPC
    {
        public Priest() : base()
        { }

        public void Interaction()
        {
            Console.WriteLine("1. Confession (save)"); // save the party
            Console.WriteLine("2. Resurrection"); // resurrect a member
            Console.WriteLine("3. Benediction"); // remove curse status effect from an afflicted party member
            Console.WriteLine("4. Purrification"); // remove poison status effect from an afflicted party member
            Console.WriteLine("5. Divination"); // states the number of XPs the party members must gain in order to reach their next levels
        }
    }
}
