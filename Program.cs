using System;

namespace SimpleTextAdventure
{
    static class Program
    {
        public static string gameName = "SimpleTextAdventure";
        public static string gameAuthor = "Nonparoxysmic";
        public static string gameVersion = "Very Early Alpha";

        static void Main()
        {
            Console.Title = gameName + " by " + gameAuthor;

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
            GameLoop gameLoop = new GameLoop(player);
            gameLoop.PlayGame();
        }

        public static void PrintWrappedText(string text, int width, string indent = "", bool doIndent = false)
        {
            string trimmedText = text.Trim();

            if (doIndent) trimmedText = indent + trimmedText;

            if (trimmedText.Length <= width)
            {
                Console.WriteLine(trimmedText);
                return;
            }

            int lineBreakWidth = width;
            for (int pos = width; pos > 0; pos--)
            {
                if (Char.IsWhiteSpace(trimmedText[pos]))
                {
                    lineBreakWidth = pos;
                    break;
                }
            }
            string firstLine = trimmedText.Substring(0, lineBreakWidth);
            string remainder = trimmedText.Substring(lineBreakWidth);

            Console.WriteLine(firstLine);
            if (indent != "") doIndent = true;
            PrintWrappedText(remainder, width, indent, doIndent);
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
