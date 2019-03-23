using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.stats;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.misc;

namespace ConsoleGame.entity
{
    public class Monster : Entity
    {
        /// <summary>
        /// Loots is an array of ValueTuple and represent the items the monster can loot
        /// - Percent is the percent of chance to drop the item
        /// - Id is the id of the item
        /// - DataPath is the path of the json where the item is
        /// </summary>
        public LootTable[] LootsTable { get; set; }

        public Monster(string name) : base(name)
        {
            /*Weapon monsterWeapon;
            EntityStats = new MonsterStats(name, out monsterWeapon, out (double percent, int id, string dataType)[] lootTable);
            monsterWeapon.Init();
            Weapon = monsterWeapon;
            LootsTable = lootTable;*/
        }

        public void Loots()
        {
            utils.Utils.Cconsole.Color("Red").WriteLine(LootsTable.Length);
            for(int i = 0; i < LootsTable.Length; ++i)
            {
                (double percent, int id, string dataType) = LootsTable[i];
                double random = RandomNumber.Between(0, 100) + new Random().NextDouble();
                
                if(random > 100)
                {
                    random = 100;
                }

                utils.Utils.Cconsole.Color("Yellow").WriteLine("{0} <= {1}", random, percent);
                if(random <= percent)
                {
                    utils.Utils.Cconsole.Color("Blue").WriteLine("id: {0} \t percent: {1}", id, percent);
                }
                else
                {
                    utils.Utils.Cconsole.Color("Red").WriteLine("id: {0} \t percent: {1}", id, percent);
                }
            }
        }
    }
}
