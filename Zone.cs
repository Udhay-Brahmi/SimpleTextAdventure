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
        public Dictionary<Direction, Item> locks = new Dictionary<Direction, Item>();
        public List<Item> items = new List<Item>();

        public bool playerHasVisited;
        public bool isDark;

        public Zone(string codeName, string name, bool isDark, string examineText)
        {
            this.codeName = codeName.ToLower();
            this.name = name;
            this.isDark = isDark;
            this.examineText = examineText;
        }

        public void PrintExamineText(bool playerHasLightSource)
        {
            if (isDark && !playerHasLightSource)
            {
                Program.PrintWrappedText("It is too dark to see anything.");
                PrintExitDirections();
            }
            else
            {
                Program.PrintWrappedText(examineText);
                PrintExitDirections();
                PrintItems();
                if (!playerHasVisited) playerHasVisited = true;
            }
        }

        public void PrintExitDirections()
        {
            string[] allExits = new string[exits.Count];
            int exitNumber = 0;
            foreach (int i in Enum.GetValues(typeof(Direction)))
            {
                if (exits.ContainsKey((Direction)i))
                {
                    allExits[exitNumber] = ((Direction)i).ToString();
                    if (locks.ContainsKey((Direction)i))
                    {
                        allExits[exitNumber] += " (Locked)";
                    }
                    exitNumber++;
                }
            }
            Program.PrintWrappedText("You can move: " + string.Join(", ", allExits));
        }

        public void PrintItems()
        {
            if (items.Count > 0)
            {
                Program.PrintWrappedText("You see: " + string.Join(", ", items));
            }
        }

        public void PrintLook(Direction direction, bool playerHasLightSource)
        {
            switch (direction)
            {
                case Direction.Invalid:
                    Program.PrintWrappedText("Not a valid direction.");
                    break;
                case Direction.Here:
                    if (isDark && !playerHasLightSource)
                    {
                        Program.PrintWrappedText("You are in " + name + ". It is too dark to see anything.");
                        PrintExitDirections();
                    }
                    else
                    {
                        Program.PrintWrappedText("You are in " + name + ".");
                        PrintExitDirections();
                        PrintItems();
                    }
                    break;
                default:
                    if (locks.ContainsKey(direction))
                    {
                        Program.PrintWrappedText("You look " + direction + ". That way is locked.");
                        return;
                    }
                    if (exits.ContainsKey(direction))
                    {
                        if (exits[direction].isDark)
                        {
                            Program.PrintWrappedText("You look " + direction + ". You see a dark area.");
                        }
                        else
                        {
                            Program.PrintWrappedText("You look " + direction + ". You see " + exits[direction].name + ".");
                        }
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
                case Direction.Up: return Direction.Down;
                case Direction.Down: return Direction.Up;
                case Direction.In: return Direction.Out;
                case Direction.Out: return Direction.In;
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

        public static void ConnectZones(Zone startZone, Direction moveDirection, Zone endZone, Item key)
        {
            ConnectZones(startZone, moveDirection, endZone);
            startZone.locks.Add(moveDirection, key);
            endZone.locks.Add(ReverseDirection(moveDirection), key);
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

        public void AddExit(Direction direction, Zone adjacentZone, Item key)
        {
            AddExit(direction, adjacentZone);
            locks.Add(direction, key);
        }
    }
}
