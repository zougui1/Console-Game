using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.utils;

namespace ConsoleGame.entity.NPC
{
    public class WeaponMerchant : AbstractNPC
    {
        public Weapon[] Weapons { get; set; }
        public int[] WeaponsId { get; set; }

        public WeaponMerchant() : base()
        { }

        public void GetWeaponsById()
        {
            Weapons = new Weapon[WeaponsId.Length];
            for (int i = 0; i < WeaponsId.Length; ++i)
            {
                Weapons[i] = Json.GetWeapon(WeaponsId[i]);
            }
        }

        public void DisplayList()
        {
            Utils.Endl();
            for (int i = 0; i < Weapons.Length; ++i)
            {
                Weapon weapon = Weapons[i];

                Console.WriteLine("{0}:   {1}", i + 1, weapon.Name);
            }
            Utils.Endl();
        }
    }
}
