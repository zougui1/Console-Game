using ConsoleGame.building;
using Newtonsoft.Json;

namespace ConsoleGame.game
{
    public class CurrentBuilding
    {
        public Building Building { get; set; }
        public Church Church { get; set; }
        public ArmorShop ArmorShop { get; set; }
        public WeaponShop WeaponShop { get; set; }
        public ItemShop ItemShop { get; set; }

        public CurrentBuilding() { }

        [JsonConstructor]
        public CurrentBuilding(
            Building building = null,
            Church church = null,
            ArmorShop armorShop = null,
            WeaponShop weaponShop = null,
            ItemShop itemShop = null
        )
        {
            Building = building;
            Church = church;
            ArmorShop = armorShop;
            WeaponShop = weaponShop;
            ItemShop = itemShop;
        }

        public Building GetRightBuilding()
        {
            if (Building != null)
            {
                return Building;
            }
            else if (Church != null)
            {
                return Church;
            }
            else if (ArmorShop != null)
            {
                return ArmorShop;
            }
            else if (WeaponShop != null)
            {
                return WeaponShop;
            }
            else if (ItemShop != null)
            {
                return ItemShop;
            }

            return null;
        }

        public string GetCurrentBuilding()
        {
            if (Building != null)
            {
                return "Building";
            }
            else if (Church != null)
            {
                return "Church";
            }
            else if (ArmorShop != null)
            {
                return "ArmorShop";
            }
            else if (WeaponShop != null)
            {
                return "WeaponShop";
            }
            else if (ItemShop != null)
            {
                return "ItemShop";
            }

            return null;
        }

        public void SetCurrentBuilding(Church church)
        {
            ResetBuildings();
            Church = church;
        }

        public void SetCurrentBuilding(ArmorShop armorShop)
        {
            ResetBuildings();
            ArmorShop = armorShop;
        }

        public void SetCurrentBuilding(WeaponShop weaponShop)
        {
            ResetBuildings();
            WeaponShop = weaponShop;
        }

        public void SetCurrentBuilding(ItemShop itemShop)
        {
            ResetBuildings();
            ItemShop = itemShop;
        }

        public void SetCurrentBuilding(Building building)
        {
            ResetBuildings();
            Building = building;
        }

        private void ResetBuildings()
        {
            Building = null;
            Church = null;
            ArmorShop = null;
            WeaponShop = null;
            ItemShop = null;
        }
    }
}
