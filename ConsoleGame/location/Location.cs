using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.building;
using ConsoleGame.entity.NPC;
using ConsoleGame.misc.coords;
using ConsoleGame.utils;

namespace ConsoleGame.location
{
    public class Location
    {
        public string Name { get; protected set; }
        public Coords Coords { get; private set; }
        public Building[] Buildings { get; protected set; }
        public int[] BuildingsId { get; set; }
        public Citizen[] Citizens { get; protected set; }
        public int[] NPCsId { get; set; }
        public string Type { get; protected set; }
        public Church Church { get; protected set; }
        public ArmorShop ArmorShop { get; set; }
        public WeaponShop WeaponShop { get; set; }
        public ItemShop ItemShop { get; set; }

        public Location(string name, Coords coords, Building[] buildings)
        {
            Name = name;
            Coords = coords;
            Buildings = buildings;
            Church = new Church();
        }

        public void GetBuildingsById()
        {
            Buildings = new Building[BuildingsId.Length];

            for (int i = 0; i < BuildingsId.Length; ++i)
            {
                Buildings[i] = Json.GetBuilding(BuildingsId[i]);
            }
        }

        public void GetNPCsById()
        {
            Utils.Cconsole.Color("Blue").WriteLine(NPCsId.Length);
            Citizens = new Citizen[NPCsId.Length];

            for (int i = 0; i < NPCsId.Length; ++i)
            {
                Citizens[i] = (Citizen)Json.GetNPC(NPCsId[i]);
            }
        }

        public void InitAllBuildings()
        {
            for(int i = 0; i < Buildings.Length; ++i)
            {
               // Buildings[i].Init();
            }
        }
    }
}
