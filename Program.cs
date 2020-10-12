using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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

            XElement worldData = XElement.Load("World.xml");

            List<XElement> zonesData = worldData.Elements().Where(x => x.Name == "Zones").ToList()[0].Elements().ToList();
            List<Zone> zoneList = new List<Zone>();
            foreach (XElement zone in zonesData)
            {
                string nameData = zone.Attribute("Name").Value;
                if (nameData.IndexOf('#') < 0) Program.PrintErrorAndExit("XML: Error in Zone Name Data");
                string name = nameData.Substring(0, nameData.IndexOf('#')) + nameData.Substring(nameData.IndexOf('#') + 1);
                string codeName = "";
                for (int i = nameData.IndexOf('#') + 1; i < nameData.Length; i++)
                {
                    if (char.IsWhiteSpace(nameData[i])) break;
                    codeName += nameData[i];
                }
                string examineText = zone.Value;
                zoneList.Add(new Zone(codeName, name, examineText));
            }

            string startingZoneName = worldData.Element("StartingZone").Value;
            Zone startingZone = zoneList.FirstOrDefault(x => x.codeName.ToLower() == startingZoneName.ToLower());
            if (startingZone == null) Program.PrintErrorAndExit("XML: Error in Starting Zone Data");

            List<XElement> connectionsData = worldData.Elements().Where(x => x.Name == "ZoneConnections").ToList()[0].Elements().ToList();
            foreach (XElement connection in connectionsData)
            {
                string start = connection.Attribute("Start").Value;
                string directionName = connection.Attribute("Direction").Value;
                string end = connection.Attribute("End").Value;

                Zone startZone = zoneList.FirstOrDefault(x => x.codeName.ToLower() == start.ToLower());
                Zone endZone = zoneList.FirstOrDefault(x => x.codeName.ToLower() == end.ToLower());
                Direction moveDirection = 0;
                if (startZone == null || endZone == null || !Parser.TryParseDirection(directionName, out moveDirection)) Program.PrintErrorAndExit("XML: Error in Zone Connection Data");

                Zone.ConnectZones(startZone, moveDirection, endZone);
            }

            Player player = new Player("Tabula Rasa", startingZone);

            List<XElement> itemData = worldData.Elements().Where(x => x.Name == "Items").ToList()[0].Elements().ToList();
            foreach (XElement item in itemData)
            {
                string nameData = item.Attribute("Name").Value;
                if (nameData.IndexOf('#') < 0) Program.PrintErrorAndExit("XML: Error in Item Name Data");
                string name = nameData.Substring(0, nameData.IndexOf('#')) + nameData.Substring(nameData.IndexOf('#') + 1);
                string codeName = "";
                for (int i = nameData.IndexOf('#') + 1; i < nameData.Length; i++)
                {
                    if (char.IsWhiteSpace(nameData[i])) break;
                    codeName += nameData[i];
                }
                string examineText = item.Value;

                if (item.Attribute("StartLocation").Value == "PLAYER")
                {
                    player.inventory.Add(new Item(codeName, name, examineText));
                }
                else
                {
                    string zoneCodeName = item.Attribute("StartLocation").Value;
                    Zone itemZone = zoneList.FirstOrDefault(x => x.codeName.ToLower() == zoneCodeName.ToLower());
                    if (itemZone == null) Program.PrintErrorAndExit("XML: Error in Item Zone Data");
                    itemZone.items.Add(new Item(codeName, name, examineText));
                }
            }
            
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
