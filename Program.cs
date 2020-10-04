using System;
using System.Linq;

namespace SimpleTextAdventure
{
    class Program
    {
        static void Main()
        {
            Console.Title = "SimpleTextAdventure by Nonparoxysmic";

            // Create Zones And Items:

            Zone study = new Zone("The Study", "It's the study.");
            Zone billiards = new Zone("The Billiards Room", "It's the billiards room.");
            Zone lounge = new Zone("The Lounge", "It's the lounge.");
            Zone library = new Zone("The Library", "It's the library.");
            Zone hall = new Zone("The Hall", "It's the hall");
            Zone dining = new Zone("The Dining Room", "It's the dining room.");
            Zone conservatory = new Zone("The Conservatory", "It's the conservatory.");
            Zone ballroom = new Zone("The Ballroom", "It's the ballroom.");
            Zone kitchen = new Zone("The Kitchen", "It's the kitchen.");

            Zone.ConnectZones(study, Direction.East, billiards);
            Zone.ConnectZones(billiards, Direction.East, lounge);
            Zone.ConnectZones(study, Direction.South, library);
            Zone.ConnectZones(billiards, Direction.South, hall);
            Zone.ConnectZones(lounge, Direction.South, dining);
            Zone.ConnectZones(library, Direction.East, hall);
            Zone.ConnectZones(hall, Direction.East, dining);
            Zone.ConnectZones(hall, Direction.South, ballroom);
            Zone.ConnectZones(dining, Direction.South, kitchen);
            Zone.ConnectZones(conservatory, Direction.East, ballroom);

            study.items.Add(new Item("a heavy lead pipe", "pipe"));
            lounge.items.Add(new Item("a length of rope", "rope"));
            library.items.Add(new Item("a revolver", "revolver"));
            conservatory.items.Add(new Item("a large wrench", "wrench"));
            ballroom.items.Add(new Item("a hefty candlestick", "candlestick"));
            kitchen.items.Add(new Item("a sharp knife", "knife"));

            // Initialize Player and Begin Game:

            Player player = new Player("Tabula Rasa", hall);
            player.PlayGame();
        }

        public static void ParseUserInput(string userInput, out Command commandOut, out string[] parametersOut)
        {
            // Found this on the internet. TODO: Understand LINQ
            string[] terms = userInput.ToLower().Split(null).Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (terms == null || terms.Length == 0) terms = new string[] { "" };

            Command command;
            switch (terms[0])
            {
                case "":
                case "help":
                case "?":
                case "commands":
                    command = Command.GameHelp;
                    break;
                case "quit":
                    command = Command.GameQuit;
                    break;
                case "version":
                    command = Command.GameVersion;
                    break;
                case "look":
                case "l":
                    command = Command.Look;
                    break;
                case "move":
                case "go":
                    command = Command.Move;
                    break;
                case "n":
                    commandOut = Command.Move;
                    parametersOut = new string[] { "North" };
                    return;
                case "e":
                    commandOut = Command.Move;
                    parametersOut = new string[] { "East" };
                    return;
                case "s":
                    commandOut = Command.Move;
                    parametersOut = new string[] { "South" };
                    return;
                case "w":
                    commandOut = Command.Move;
                    parametersOut = new string[] { "West" };
                    return;
                default:
                    command = Command.Invalid;
                    break;
            }
            commandOut = command;

            string[] parameters;
            if (terms.Length == 1) parameters = new string[] { };
            else
            {
                parameters = new string[terms.Length - 1];
                for (int i = 0; i < parameters.Length; i++)
                {
                    parameters[i] = terms[i + 1];
                }
            }
            parametersOut = parameters;
        }

        public static bool TryParseDirection(string input, out Direction directionOut)
        {
            Direction direction;
            bool isValidDirection = true;
            switch (input.ToLower())
            {
                case "n":
                case "north":
                    direction = Direction.North;
                    break;
                case "e":
                case "east":
                    direction = Direction.East;
                    break;
                case "s":
                case "south":
                    direction = Direction.South;
                    break;
                case "w":
                case "west":
                    direction = Direction.West;
                    break;
                default:
                    isValidDirection = false;
                    direction = 0;
                    break;
            }
            directionOut = direction;
            return isValidDirection;
        }

        public static void PrintErrorAndExit(string message = "Unknown Error")
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ERROR: " + message);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
