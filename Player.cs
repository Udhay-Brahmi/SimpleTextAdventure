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
                Console.WriteLine("You are in " + currentZone.briefDescription + ".");
                Console.WriteLine("You can move: " + currentZone.ListOfExitDirections());
                if (currentZone.items.Count > 0)
                {
                    Console.WriteLine("You see: " + currentZone.items[0].briefDescription);
                }
            }
            else
            {
                if (Parser.TryParseDirection(parameters[0], out Direction direction))
                {
                    if (currentZone.exits.ContainsKey(direction))
                    {
                        Console.WriteLine("You look " + direction + ". You see " + currentZone.exits[direction].briefDescription + ".");
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
                        Console.WriteLine("You arrive at " + currentZone.briefDescription + ".");
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

        public void ExamineAction(string[] parameters)
        {
            if (parameters.Length == 0)
            {
                Console.WriteLine("This command requires a target.");
            }
            else
            {
                if (currentZone.items.Count > 0 && currentZone.items[0].referenceName == parameters[0])
                {
                    Console.WriteLine(currentZone.items[0].GetExamineText());
                }
                else if (currentZone.referenceName == parameters[0])
                {
                    Console.WriteLine(currentZone.GetExamineText());
                }
                else
                {
                    Console.WriteLine("Unrecognized target.");
                }
            }
        }
    }
}
