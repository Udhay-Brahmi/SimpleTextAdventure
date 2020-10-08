using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class Zone
    {
        public string codeName;
        public string name;
        public string examineText;

        public Dictionary<Direction, Zone> exits = new Dictionary<Direction, Zone>();
        public List<Item> items = new List<Item>();

        public Zone(string codeName, string name, string examineText)
        {
            this.codeName = codeName;
            this.name = name;
            this.examineText = examineText;
        }

        public void PrintExamineText()
        {
            Program.PrintWrappedText("You examine " + name + ". " + examineText);
            PrintExitDirections();
            PrintItems();
        }

        public void PrintExitDirections()
        {
            string[] output = new string[exits.Count];
            int exitNumber = 0;
            foreach (int i in Enum.GetValues(typeof(Direction)))
            {
                if (exits.ContainsKey((Direction)i))
                {
                    output[exitNumber] = ((Direction)i).ToString();
                    exitNumber++;
                }
            }
            Program.PrintWrappedText("You can move: " + string.Join(", ", output));
        }

        public void PrintItems()
        {
            if (items.Count > 0)
            {
                Program.PrintWrappedText("You see: " + string.Join(", ", items));
            }
        }

        public void PrintLook(Direction direction)
        {
            switch (direction)
            {
                case Direction.Invalid:
                    Program.PrintWrappedText("Not a valid direction.");
                    break;
                case Direction.Here:
                    Program.PrintWrappedText("You are in " + name + ".");
                    PrintExitDirections();
                    PrintItems();
                    break;
                default:
                    if (exits.ContainsKey(direction))
                    {
                        Program.PrintWrappedText("You look " + direction + ". You see " + exits[direction].name + ".");
                    }
                    else
                    {
                        Program.PrintWrappedText("There's nothing that way.");
                    }
                    break;
            }
        }

        public static Direction ReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.North: return Direction.South;
                case Direction.East: return Direction.West;
                case Direction.South: return Direction.North;
                case Direction.West: return Direction.East;
                default:
                    Program.PrintErrorAndExit("Zone: Invalid direction in ReverseDirection()");
                    return Direction.Invalid;
            }
        }

        public static void ConnectZones(Zone startZone, Direction moveDirection, Zone endZone)
        {
            startZone.AddExit(moveDirection, endZone);
            endZone.AddExit(ReverseDirection(moveDirection), startZone);
        }

        public void AddExit(Direction direction, Zone adjacentZone)
        {
            if (adjacentZone == null)
            {
                Program.PrintErrorAndExit("Attempted to add invalid exit to Zone: " + this.name);
            }
            try
            {
                exits.Add(direction, adjacentZone);
            }
            catch (ArgumentException)
            {
                Program.PrintErrorAndExit("Attempted to add duplicate exit to Zone: " + this.name);
            }
        }
    }
}
