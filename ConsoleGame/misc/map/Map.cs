using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.game;
using ConsoleGame.json;
using ConsoleGame.utils;

namespace ConsoleGame.misc.map
{
    public class Map
    {
        public short MaxY { get; private set; }
        public short MaxX { get; private set; }
        public short MinY { get; private set; }
        public short MinX { get; private set; }
        public Zone CurrentZone { get; private set; }

        public Map(Zone zone)
        {
            MaxX = 10_000;
            MaxY = 10_000;
            MinY = -10_000;
            MinX = -10_000;
            CurrentZone = zone;
        }

        [JsonConstructor]
        public Map(short maxY, short maxX, short minY, short minX, Zone currentZone)
        {
            MaxY = maxY;
            MaxX = maxX;
            MinY = minY;
            MinX = minX;
            CurrentZone = currentZone;
        }

        public void ChangeZoneIfNeeded(User user)
        {
            Zone newZone = Json.GetCurrentZone(user, CurrentZone);

            if (CurrentZone != newZone)
            {
                /*Utils.Cconsole.Color("Red").WriteLine("---------------------------")
                    .Multi(console => console.Color("Cyan")
                        .WriteLine("X: {0}", newZone.Coords.X)
                        .WriteLine("Y: {0}", newZone.Coords.Y)
                        .WriteLine("Width: {0}", newZone.Rect.Width)
                        .WriteLine("Height: {0}", newZone.Rect.Height)
                    )
                    .Color("Red").WriteLine("---------------------------");*/

                CurrentZone = newZone;
            }
        }
    }
}
