using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.misc.coords;
using ConsoleGame.entity;

namespace ConsoleGame.location
{
    public class Location
    {
        public string Name { get; protected set; }
        public Coords Coords { get; private set; }
        public dynamic[] Buildings { get; protected set; }
        public int[] BuildingsId { get; set; }
        public NPC[] NPCs { get; protected set; }
        public int[] NPCsId { get; set; }
        public string Type { get; protected set; }

        public Location(string name, Coords coords, dynamic[] buildings)
        {
            Name = name;
            Coords = coords;
            Buildings = buildings;
        }

        public void GetBuildingsById()
        {
            Buildings = new dynamic[BuildingsId.Length];

            for (int i = 0; i < BuildingsId.Length; ++i)
            {
                Buildings[i] = Json.GetBuilding(BuildingsId[i]);
            }
        }

        public void GetNPCsById()
        {
            Utils.Cconsole.Color("Blue").WriteLine(NPCsId.Length);
            NPCs = new NPC[NPCsId.Length];

            for (int i = 0; i < NPCsId.Length; ++i)
            {
                NPCs[i] = Json.GetNPC(NPCsId[i]);
            }
        }

        public void InitAllBuildings()
        {
            for(int i = 0; i < Buildings.Length; ++i)
            {
                Buildings[i].Init();
            }
        }
    }
}
