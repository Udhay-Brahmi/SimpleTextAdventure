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

        public void LookAction(Parameter[] parameters)
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
            else if (parameters[0].type == ParameterType.Direction)
            {
                if (currentZone.exits.ContainsKey(parameters[0].directionParameter))
                {
                    Console.WriteLine("You look " + parameters[0].directionParameter + ". You see " + currentZone.exits[parameters[0].directionParameter].briefDescription + ".");
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

        public void ExamineAction(Parameter[] parameters)
        {
            if (parameters.Length == 0)
            {
                Console.WriteLine("This command requires a target.");
            }
            else
            {
                if (currentZone.items.Count > 0 && currentZone.items[0].referenceName == parameters[0].stringParameter)
                {
                    Console.WriteLine(currentZone.items[0].GetExamineText());
                }
                else if (currentZone.referenceName == parameters[0].stringParameter)
                {
                    Console.WriteLine(currentZone.GetExamineText());
                }
                else
                {
                    bool adjacentZoneFound = false;
                    foreach (KeyValuePair<Direction,Zone> exit in currentZone.exits)
                    {
                        if (exit.Value.referenceName == parameters[0].stringParameter)
                        {
                            Console.WriteLine("You are too far away to examine " + exit.Value.briefDescription + ".");
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
