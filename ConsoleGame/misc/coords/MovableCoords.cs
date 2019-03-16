using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.location;
using ConsoleGame.location.locationTypes;

namespace ConsoleGame.misc.coords
{
    public class MovableCoords : Coords
    {

        public MovableCoords(int x = 0, int y = 0) : base(x, y)
        { }

        public void NumberMove(object[] args)
        {
            Utils.Cconsole.Color("DarkGray").WriteLine("How many time do you want to move? (1-100)");
            string parameter = "range:1-100";
            int movements = 0;
            movements = Utils.TryParseConsoleCin("Please enter a valid number (between 1 and 100 included)", parameter, "DarkRed");
            Moves(movements, (int)args[0]);
        }

        public void Moves(int movements, int direction)
        {
            for (int i = 0; i < movements; ++i)
            {
                if (Move(direction))
                {
                    return;
                }
            }
            ShowCoords();
            Console.WriteLine("Nothing happened.");

            Game.ChooseAction();
        }

        public bool Move(int direction)
        {
            Utils.Caller(this, "Move" + (Directions)direction);

            if (LocationList.LocationsDict.ContainsKey((X: X, Y: Y)))
            {
                dynamic currentLocation = Json.GetLocation(LocationList.LocationsDict[(X: X, Y: Y)]);
                currentLocation.GetBuildingsById();
                currentLocation.GetNPCsById();
                currentLocation.InitAllBuildings();

                switch (currentLocation.Type)
                {
                    case "Town":
                        Game.CurrentTown = (Town)currentLocation;
                        break;
                    case "City":
                        Game.CurrentCity = (City)currentLocation;
                        break;
                    case "Kingdom":
                        Game.CurrentKingdom = (Kingdom)currentLocation;
                        break;
                    default: break;
                }
                
                Console.WriteLine("You entered in \"{0}\"", currentLocation.Name);
                Game.CurrentLocationType = currentLocation.Type;

                Game.InCity();
                return true;
            }

            int number = RandomNumber.Between(0, 100);
            if (number <= Game.PercentOfMonster)
            {
                Game.TriggerMonster();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MoveUp()
        {
            --Y;
        }

        public void MoveDown()
        {
            ++Y;
        }

        public void MoveLeft()
        {
            --X;
        }

        public void MoveRight()
        {
            ++X;
        }

        public void ShowCoords()
        {
            Console.WriteLine("X: {0}, Y: {1}", X, Y);
        }
    }
}
