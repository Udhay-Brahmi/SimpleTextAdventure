using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class Player
    {
        public string name;
        public Zone currentZone;
        //public List<Item> inventory;

        public Player(string name, Zone startingZone)
        {
            this.name = name;
            currentZone = startingZone;
        }

        public void LookAction(string[] parameters)
        {
            if (parameters.Length == 0)
            {
                Console.WriteLine("You look around. " + currentZone.description);
                Console.WriteLine("You can move: " + currentZone.ListOfExitDirections());
                if (currentZone.items.Count > 0)
                {
                    Console.WriteLine("You see: " + currentZone.items[0].longName);
                }
            }
            else
            {
                if (Parser.TryParseDirection(parameters[0], out Direction direction))
                {
                    if (currentZone.exits.ContainsKey(direction))
                    {
                        Console.Write("You look " + direction + ". ");
                        Console.WriteLine(currentZone.exits[direction].description);
                    }
                    else
                    {
                        Console.WriteLine("There's nothing that way.");
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid direction.");
                }
            }
        }

        public void MoveAction(string[] parameters)
        {
            if (parameters.Length == 0)
            {
                Console.WriteLine("You must specify a direction to move.");
            }
            else
            {
                if (Parser.TryParseDirection(parameters[0], out Direction direction))
                {
                    if (currentZone.exits.ContainsKey(direction))
                    {
                        Console.Write("You move " + direction + ". ");
                        currentZone = currentZone.exits[direction];
                        Console.WriteLine("You arrive at " + currentZone.name);
                    }
                    else
                    {
                        Console.WriteLine("You can't move that way.");
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid direction.");
                }
            }
        }
    }
}
