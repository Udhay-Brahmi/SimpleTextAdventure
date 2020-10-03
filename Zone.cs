using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class Zone
    {
        public string name;
        public Dictionary<Direction, Zone> exits = new Dictionary<Direction, Zone>();
        public List<Item> items = new List<Item>();

        public Zone(string name)
        {
            this.name = name;
        }

        public static Direction ReverseDirection(Direction direction)
        {
            return (Direction)(((int)direction + 2) % 4);
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
        }

        public string ListOfExitDirections()
        {
            string output = "";
            bool printComma = false;
            for (int i = 0; i < 4; i++)
            {
                Direction dir = (Direction)i;
                if (exits.ContainsKey(dir))
                {
                    if (printComma) output += ", ";
                    output += dir.ToString();
                    printComma = true;
                }
            }
            return output;
        }
    }
}
