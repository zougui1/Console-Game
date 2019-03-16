using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.items.stuff.handed.weapons;

namespace ConsoleGame.building
{
    public class WeaponShop : Shop
    {
        public Weapon[] Weapons { get; set; }
        public int[] WeaponsId { get; set; }

        public WeaponShop() : base()
        {

        }

        public void DisplayList()
        {
            for (int i = 0; i < Weapons.Length; ++i)
            {
                Weapon weapon = Weapons[i];

                Console.WriteLine("{0}:   {1}, {2} damages", i, weapon.Name, weapon.Damages);
            }
        }

        public void Init()
        {
            GetWeaponsById();
        }

        public void GetWeaponsById()
        {
            Weapons = new Weapon[WeaponsId.Length];

            for (int i = 0; i < WeaponsId.Length; ++i)
            {
                Weapons[i] = Json.GetWeapon(int.Parse(WeaponsId[i].ToString()));
            }
        }
    }
}
