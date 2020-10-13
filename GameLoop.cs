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
            Program.PrintWrappedText("Introduction text. Type a command.");
            Console.WriteLine();
            Program.PrintWrappedText("You are in " + player.currentZone.name + ".");
            player.currentZone.PrintExamineText();

            while (true)
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
                        PrintGameHelp();
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
                    default:
                        Program.PrintWrappedText("Unrecognized command. Type \"help\" for a list of commands.");
                        break;
                }
            }
        }

        void PrintGameHelp()
        {
            Program.PrintWrappedText("List of Commands: quit, help, version, look, move / go, examine, wait, inventory, take, drop.");
            Program.PrintWrappedText("Commands with Parameters: look <direction>, move <direction>, examine <target>, take <item>, drop <item>.");
            Program.PrintWrappedText("Quick Commands: l (look), i (inventory), n (north / move north), etc.");
        }

        void PrintGameVersion()
        {
            Program.PrintWrappedText(Program.gameName + " by " + Program.gameAuthor);
            Program.PrintWrappedText("Version: " + Program.gameVersion);
        }
    }
}
