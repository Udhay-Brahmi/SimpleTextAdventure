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
            Console.WriteLine("Introduction text. Type a command.");

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
                        Console.WriteLine("List of Commands: quit, help, version, look, move / go, examine.");
                        Console.WriteLine("Commands with Parameters: look <direction>, move <direction>, examine <target>");
                        Console.WriteLine("Quick Commands: l (look), n (north / move north), etc.");
                        break;
                    case Command.GameVersion:
                        Console.WriteLine(Program.gameName + " by " + Program.gameAuthor);
                        Console.WriteLine("Version: " + Program.gameVersion);
                        break;
                    case Command.Look:
                        player.LookAction(parameters);
                        break;
                    case Command.Move:
                        player.MoveAction(parameters);
                        break;
                    case Command.Examine:
                        player.ExamineAction(parameters);
                        break;
                    default:
                        Console.WriteLine("Unrecognized command. Type \"help\" for a list of commands.");
                        break;
                }
            }
        }
    }
}
