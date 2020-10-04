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

        public void PlayGame()
        {
            Console.WriteLine("Introduction text. Type a command.");

            while (true)
            {
                Console.Write(Environment.NewLine + "> ");
                Program.ParseUserInput(Console.ReadLine(), out Command command, out string[] parameters);

                switch (command)
                {
                    case Command.GameHelp:
                        Console.WriteLine("List of Commands: help, quit, version, look, move / go.");
                        Console.WriteLine("Commands with Parameters: look <direction>, move <direction>");
                        Console.WriteLine("Quick Commands: l (look), n (north / move north), etc.");
                        break;
                    case Command.GameQuit:
                        Environment.Exit(0);
                        break;
                    case Command.GameVersion:
                        Console.WriteLine("SimpleTextAdventure by Nonparoxysmic");
                        Console.WriteLine("There's no version numbers yet.");
                        break;
                    case Command.Look:
                        LookAction(parameters);
                        break;
                    case Command.Move:
                        MoveAction(parameters);
                        break;
                    default:
                        Console.WriteLine("I don't recognize that command. Try 'help'.");
                        break;
                }
            }
        }

        void LookAction(string[] parameters)
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
                if (Program.TryParseDirection(parameters[0], out Direction direction))
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

        void MoveAction(string[] parameters)
        {
            if (parameters.Length == 0)
            {
                Console.WriteLine("You must specify a direction to move.");
            }
            else
            {
                if (Program.TryParseDirection(parameters[0], out Direction direction))
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
