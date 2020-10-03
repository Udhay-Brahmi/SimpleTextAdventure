using System;

namespace SimpleTextAdventure
{
    class Program
    {
        static void Main()
        {
            // Create Zones And Items:

            Zone swamp = new Zone("The Swamp");
            Zone cave = new Zone("The Cave");
            Zone plains = new Zone("The Plains");
            Zone forest = new Zone("The Forest");
            Zone ruins = new Zone("The Ruins");
            Zone mountain = new Zone("The Mountain");

            Zone.ConnectZones(swamp, Direction.East, cave);
            Zone.ConnectZones(cave, Direction.East, plains);
            Zone.ConnectZones(swamp, Direction.South, forest);
            Zone.ConnectZones(cave, Direction.South, ruins);
            Zone.ConnectZones(plains, Direction.South, mountain);
            Zone.ConnectZones(forest, Direction.East, ruins);
            Zone.ConnectZones(ruins, Direction.East, mountain);

            swamp.items.Add(new Item("a muddy STICK", "STICK"));
            cave.items.Add(new Item("some MOSS", "MOSS"));
            plains.items.Add(new Item("a SUNFLOWER", "SUNFLOWER"));
            forest.items.Add(new Item("some tree BARK", "BARK"));
            ruins.items.Add(new Item("a BRICK", "BRICK"));
            mountain.items.Add(new Item("a sharp ROCK", "ROCK"));

            // Initialize Player and Begin Game:

            Player player = new Player("Tabula Rasa", plains);
            player.PlayGame();
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
