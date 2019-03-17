using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.stats;
using ConsoleGame.items.stuff.handed.weapons;

namespace ConsoleGame.entity
{
    public class Monster : Entity
    {
        public Monster(string name) : base(name)
        {
            Weapon monsterWeapon;
            EntityStats = new MonsterStats(name, out monsterWeapon);
            monsterWeapon.Init();
            Weapon = monsterWeapon;
        }
    }
}
