using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleGame.entity.stats
{
    public class InitStats : EntityStats
    {
        public int InitHealth { get; set; }
        public int InitMana { get; set; }

        public InitStats() : base()
        { }

        public void Init()
        {
            MaxHealth = InitHealth;
            Health = InitHealth;
            MaxMana = InitMana;
            Mana = InitMana;
        }
    }
}
