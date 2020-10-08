using System;

namespace SimpleTextAdventure
{
    static class Program
    {
        public static string gameName = "SimpleTextAdventure";
        public static string gameAuthor = "Nonparoxysmic";
        public static string gameVersion = "Very Early Alpha";

        static readonly string wrappingIndent = "  ";
        static readonly bool indentFirstLine = false;

        static void Main()
        {
            Console.Title = gameName + " by " + gameAuthor;

            // Create Zones And Items:

            Zone study = new Zone("study", "the STUDY", "A spacious office with a large desk and a fireplace.");
            Zone billiard = new Zone("billiard", "the BILLIARD Room", "A gaming room featuring a billiard table.");
            Zone lounge = new Zone("lounge", "the LOUNGE", "A relaxing space.");
            Zone library = new Zone("library", "the LIBRARY", "The walls are lined with bookcases.");
            Zone hall = new Zone("hall", "the HALL", "The main hall of the building.");
            Zone dining = new Zone("dining", "the DINING Room", "The large dining table could seat six or seven.");
            Zone conservatory = new Zone("conservatory", "the CONSERVATORY", "A humid sunroom with a variety of plants.");
            Zone ballroom = new Zone("ballroom", "the BALLROOM", "An opulent ballroom, empty save for a grand piano.");
            Zone kitchen = new Zone("kitchen", "the KITCHEN", "Yep. It's a kitchen.");

            Zone.ConnectZones(study, Direction.East, billiard);
            Zone.ConnectZones(billiard, Direction.East, lounge);
            Zone.ConnectZones(study, Direction.South, library);
            Zone.ConnectZones(billiard, Direction.South, hall);
            Zone.ConnectZones(lounge, Direction.South, dining);
            Zone.ConnectZones(library, Direction.East, hall);
            Zone.ConnectZones(hall, Direction.East, dining);
            Zone.ConnectZones(hall, Direction.South, ballroom);
            Zone.ConnectZones(dining, Direction.South, kitchen);
            Zone.ConnectZones(conservatory, Direction.East, ballroom);

            study.items.Add(new Item("pipe", "a heavy lead PIPE", "A section of old bent pipe made of lead. Fairly heavy."));
            lounge.items.Add(new Item("rope", "a length of ROPE", "A short length of sturdy rope with some fraying near the middle."));
            library.items.Add(new Item("revolver", "a REVOLVER", "A handgun with a six-chamber cylinder. One round has already been fired."));
            conservatory.items.Add(new Item("wrench", "a large WRENCH", "A very large wrench with some damage. Looks like it was dropped."));
            ballroom.items.Add(new Item("candlestick", "a hefty CANDLESTICK", "A large golden candlestick. It was cleaned recently."));
            kitchen.items.Add(new Item("knife", "a sharp KNIFE", "A dagger with a long sharp blade. The very tip is broken off."));

            // Initialize Player and Begin Game:

            Player player = new Player("Tabula Rasa", hall);
            GameLoop gameLoop = new GameLoop(player);
            gameLoop.PlayGame();
        }

        public static void PrintWrappedText(string text)
        {
            PrintWrappedText(text, Console.WindowWidth - 1, wrappingIndent, indentFirstLine);
        }

        public static void PrintWrappedText(string text, int width, string indent = "", bool doIndent = false)
        {
            string trimmedText = text.Trim();

            int indentLength = 0;
            if (doIndent)
            {
                trimmedText = indent + trimmedText;
                indentLength = indent.Length;
            }

            if (trimmedText.Length <= width)
            {
                Console.WriteLine(trimmedText);
                return;
            }

            int lineBreakWidth = width;
            for (int pos = width; pos > indentLength; pos--)
            {
                if (Char.IsWhiteSpace(trimmedText[pos]))
                {
                    lineBreakWidth = pos;
                    break;
                }
            }
            string firstLine = trimmedText.Substring(0, lineBreakWidth).TrimEnd();
            string remainder = trimmedText.Substring(lineBreakWidth);

            Console.WriteLine(firstLine);
            if (indent != "") doIndent = true;
            PrintWrappedText(remainder, width, indent, doIndent);
        }

        public static void PrintErrorAndExit(string message = "Unknown Error")
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Program.PrintWrappedText("ERROR: " + message);
            Program.PrintWrappedText("Press any key to exit.");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
