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

        public override void Init()
        {
            Health = InitHealth;
            MaxHealth = InitHealth;
            Mana = InitMana;
            MaxMana = InitMana;
        }
    }
}
