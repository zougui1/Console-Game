using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.json;
using ConsoleGame.location;
using ConsoleGame.utils;

namespace ConsoleGame.misc.coords
{
    public class MovableCoords : Coords
    {

        public MovableCoords(int x = 0, int y = 0) : base(x, y)
        { }

        /// <summary>
        /// NumberMove is used to let the user choose how many time they want to move
        /// </summary>
        /// <param name="args">the index 0 must contains the direction of the movements (must be of type Directions or int)</param>
        public void NumberMove(object[] args)
        {
            Utils.Cconsole.Color("DarkGray").WriteLine("How many time do you want to move? (1-100)");
            string parameter = "range:1-100";
            int movements = 0;
            movements = Utils.TryParseConsoleCin("Please enter a valid number (between 1 and 100 included)", parameter, "DarkRed");
            Moves(movements, (int)args[0]);
        }

        /// <summary>
        /// Moves is used to move the user the amount of times its given and in the given direction
        /// </summary>
        /// <param name="movements">number of movements</param>
        /// <param name="direction">the direction to move</param>
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

            GameStatement.Game.ChooseAction();
        }

        /// <summary>
        /// Move is used to move the user, and can trigger some event
        /// - if the user move to the coords of a location, then we retrieve the location from the json and set it as current location in the Game object
        /// - can trigger a monster depending of the percent of chance to trigger one
        /// </summary>
        /// <param name="direction">the direction to move</param>
        /// <returns>return true if an event has been triggered, otherwise false</returns>
        public bool Move(int direction)
        {
            Utils.Caller(this, "Move" + (Directions)direction);

            if (LocationList.LocationsDict.ContainsKey((X: X, Y: Y)))
            {
                Location currentLocation = Json.GetLocation(LocationList.LocationsDict[(X: X, Y: Y)]);
                currentLocation.InitAllBuildings();

                GameStatement.Game.CurrentLocation = currentLocation;
                
                Console.WriteLine("You entered in \"{0}\"", currentLocation.Name);

                GameStatement.Game.InLocation();
                return true;
            }

            int number = RandomNumber.Between(0, 100);
            if (number <= GameStatement.Game.PercentOfMonster)
            {
                GameStatement.Game.TriggerMonster();
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
