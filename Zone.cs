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
            Program.PrintWrappedText("You examine " + name + ". " + examineText, Console.WindowWidth - 1);
            PrintExitDirections();
            if (items.Count > 0)
            {
                Program.PrintWrappedText("You see: " + string.Join(", ", items), Console.WindowWidth - 1);
            }
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
            Program.PrintWrappedText("You can move: " + string.Join(", ", output), Console.WindowWidth - 1);
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
            try
            {
                exits.Add(direction, adjacentZone);
            }
            catch (ArgumentException)
            {
                Program.PrintErrorAndExit("Attempted to add duplicate exit to Zone: " + this.name);
            }
            if (adjacentZone == null)
            {
                Program.PrintErrorAndExit("Attempted to add invalid exit to Zone: " + this.name);
            }
        }
    }
}
