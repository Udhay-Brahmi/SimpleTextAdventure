using System;

namespace SimpleTextAdventure
{
    class GameLoop
    {
        readonly Player player;
        int commandNumber;

        public GameLoop(Player player)
        {
            this.player = player;
        }

        public void PlayGame()
        {
            Program.PrintWrappedText("Introduction text. Type a command.");

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
                    default:
                        Program.PrintWrappedText("Unrecognized command. Type \"help\" for a list of commands.");
                        break;
                }
            }
        }

        void PrintGameHelp()
        {
            Program.PrintWrappedText("List of Commands: quit, help, version, look, move / go, examine, wait, inventory.");
            Program.PrintWrappedText("Commands with Parameters: look <direction>, move <direction>, examine <target>");
            Program.PrintWrappedText("Quick Commands: l (look), i (inventory), n (north / move north), etc.");
        }

        void PrintGameVersion()
        {
            Program.PrintWrappedText(Program.gameName + " by " + Program.gameAuthor);
            Program.PrintWrappedText("Version: " + Program.gameVersion);
        }
    }
}
