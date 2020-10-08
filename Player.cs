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
            currentZone.PrintLook(direction);
        }

        public void MoveAction(Direction direction)
        {
            switch (direction)
            {
                case Direction.Invalid:
                    Program.PrintWrappedText("Not a valid direction.");
                    break;
                case Direction.Here:
                    Program.PrintWrappedText("You must specify a direction to move.");
                    break;
                default:
                    if (currentZone.exits.ContainsKey(direction))
                    {
                        Program.PrintWrappedText("You move " + direction + ". ");
                        currentZone = currentZone.exits[direction];
                        Program.PrintWrappedText("You arrive at " + currentZone.name + ".");
                    }
                    else
                    {
                        Program.PrintWrappedText("You can't move that way.");
                    }
                    break;
            }
        }

        public void ExamineAction(Parameter[] parameters)
        {
            if (parameters.Length == 0)
            {
                Program.PrintWrappedText("This command requires a target.");
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
                            Program.PrintWrappedText("You are too far away to examine " + exit.Value.name + ".");
                            adjacentZoneFound = true;
                        }
                    }
                    if (!adjacentZoneFound)
                    {
                        Program.PrintWrappedText("Unrecognized target.");
                    }
                }
            }
        }
    }
}
