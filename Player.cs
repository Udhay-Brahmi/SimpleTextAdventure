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

        public void LookAction(Direction direction)
        {
            switch (direction)
            {
                case Direction.Here:
                    Console.WriteLine("You are in " + currentZone.name + ".");
                    currentZone.PrintExitDirections();
                    if (currentZone.items.Count > 0)
                    {
                        Console.WriteLine("You see: " + currentZone.items[0].name);
                    }
                    break;
                case Direction.Invalid:
                    Console.WriteLine("Not a valid direction.");
                    break;
                default:
                    if (currentZone.exits.ContainsKey(direction))
                    {
                        Console.WriteLine("You look " + direction + ". You see " + currentZone.exits[direction].name + ".");
                    }
                    else
                    {
                        Console.WriteLine("There's nothing that way.");
                    }
                    break;
            }
        }

        public void MoveAction(Parameter[] parameters)
        {
            if (parameters.Length == 0)
            {
                Console.WriteLine("You must specify a direction to move.");
            }
            else if (parameters[0].type == ParameterType.Direction)
            {
                if (currentZone.exits.ContainsKey(parameters[0].directionParameter))
                {
                    Console.Write("You move " + parameters[0].directionParameter + ". ");
                    currentZone = currentZone.exits[parameters[0].directionParameter];
                    Console.WriteLine("You arrive at " + currentZone.name + ".");
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

        public void ExamineAction(Parameter[] parameters)
        {
            if (parameters.Length == 0)
            {
                Console.WriteLine("This command requires a target.");
            }
            else
            {
                if (currentZone.items.Count > 0 && currentZone.items[0].codeName == parameters[0].stringParameter)
                {
                    currentZone.items[0].PrintExamineText();
                }
                else if (currentZone.codeName == parameters[0].stringParameter)
                {
                    currentZone.PrintExamineText();
                }
                else
                {
                    bool adjacentZoneFound = false;
                    foreach (KeyValuePair<Direction,Zone> exit in currentZone.exits)
                    {
                        if (exit.Value.codeName == parameters[0].stringParameter)
                        {
                            Console.WriteLine("You are too far away to examine " + exit.Value.name + ".");
                            adjacentZoneFound = true;
                        }
                    }
                    if (!adjacentZoneFound)
                    {
                        Console.WriteLine("Unrecognized target.");
                    }
                }
            }
        }
    }
}
