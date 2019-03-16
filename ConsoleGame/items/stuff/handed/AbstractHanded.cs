using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.items.stuff.handed
{
    public abstract class AbstractHanded : AbstractStuff
    {
        public bool TwoHanded { get; set; }
        public int Damages { get; set; }

        public AbstractHanded(string name, string description, int damages = 0) : base(name, description)
        {
            Damages = damages;
            TwoHanded = false;
        }

        public void Display()
        {
            Console.WriteLine("Weapon: {0} (damages: {1})", Name, Damages);
        }
        public void Init()
        {
            if (Utils.InEnum(Type, typeof(TwoHanded)))
            {
                TwoHanded = true;
            }
        }
    }
}