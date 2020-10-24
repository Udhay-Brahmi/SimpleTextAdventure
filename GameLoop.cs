using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class GameLoop
    {
        readonly Player player;
        int commandNumber;
        public List<Item> inactiveItems = new List<Item>();

        public GameLoop(Player player)
        {
            this.player = player;
        }

        public void PlayGame()
        {
            // Testing intro:
            int testNumberOfItems = 9;
            Program.PrintWrappedText("TESTING MODE // Goal: take " + testNumberOfItems + " items to the study.");
            Console.WriteLine();

            Program.PrintWrappedText("~~Intro text to be added~~");
            Console.WriteLine();
            Program.PrintWrappedText("You are in " + player.currentZone.name + ".");
            player.currentZone.PrintExamineText(player.hasLightSource);
            
            bool gameOver = false;
            while (!gameOver)
            {
                commandNumber++;
                Console.Write(Environment.NewLine + "[" + commandNumber + "] > ");
                Parser.ParseUserInput(Console.ReadLine(), out Command command, out Parameter[] parameters);

                switch (command)
                {
                    case Command.GameQuit:
                        Environment.Exit(0);
                        break;
                    case Command.GameHelp:
                        PrintGameHelp(parameters);
                        break;
                    case Command.GameVersion:
                        PrintGameVersion();
                        break;
                    case Command.Look:
                        player.LookAction(Parser.ParseDirectionParameter(parameters));
                        break;
                    case Command.Move:
                        player.MoveAction(Parser.ParseDirectionParameter(parameters));
                        break;
                    case Command.Examine:
                        player.ExamineAction(parameters);
                        break;
                    case Command.Wait:
                        Program.PrintWrappedText("You wait. Nothing interesting happens.");
                        break;
                    case Command.Inventory:
                        player.PrintInventory();
                        break;
                    case Command.Take:
                        player.TakeAction(parameters);
                        break;
                    case Command.Drop:
                        player.DropAction(parameters);
                        break;
                    case Command.Use:
                        player.UseAction(parameters);
                        break;
                    case Command.Combine:
                        player.CombineAction(parameters);
                        break;
                    default:
                        Program.PrintWrappedText("Unrecognized command. Type \"help\" for a list of commands.");
                        break;
                }

                // Testing game over condition:
                if (player.currentZone.codeName == "study" && (player.inventory.Count + player.currentZone.items.Count) >= testNumberOfItems)
                {
                    gameOver = true;
                    Console.WriteLine();
                    if (player.inventory.Count + player.currentZone.items.Count == testNumberOfItems)
                    {
                        Program.PrintWrappedText("TESTING MODE // You win! You have brought " + testNumberOfItems + " items to the study.");
                    }
                    else
                    {
                        Program.PrintWrappedText("TESTING MODE // You win! You have brought more than " + testNumberOfItems + " items to the study.");
                    }
                }
            }

            Console.Write(Environment.NewLine + "GAME OVER" + Environment.NewLine + "Press any key to exit");
            Console.ReadKey(true);
        }
        
        void PrintGameHelp(Parameter[] parameters)
        {
            if (parameters.Length == 1 && parameters[0].type == ParameterType.String && parameters[0].stringParameter == "BLANK")
            {
                Program.PrintWrappedText("Type \"help\" for a list of commands.");
                return;
            }
            Program.PrintWrappedText("List of Commands:");
            Program.PrintWrappedText("- Menu Commands: quit, help, version");
            Program.PrintWrappedText("- Basic Commands: look, move, examine, wait");
            Program.PrintWrappedText("- Item Commands: inventory, take, drop, use, combine");
            Program.PrintWrappedText("Variations:");
            Program.PrintWrappedText("- look, look <direction>, l <direction>");
            Program.PrintWrappedText("- move <direction>, go <direction>, <direction>");
            Program.PrintWrappedText("- examine <target>, x <target>");
            Program.PrintWrappedText("- inventory, i");
            Program.PrintWrappedText("- take <item>, take all");
            Program.PrintWrappedText("- drop <item>, drop all");
            Program.PrintWrappedText("- use <item>");
            Program.PrintWrappedText("- combine <item> <item>");
            Program.PrintWrappedText("Directions:");
            Program.PrintWrappedText("- north, n, east, e, south, s, west, w, up, u, down, d, in, out");
        }

        void PrintGameVersion()
        {
            Program.PrintWrappedText(Program.gameName + " by " + Program.gameAuthor);
            Program.PrintWrappedText("Version: " + Program.gameVersion);
        }
    }
}
