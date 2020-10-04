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
                Parser.ParseUserInput(Console.ReadLine(), out Command command, out string[] parameters);

                switch (command)
                {
                    case Command.GameHelp:
                        Console.WriteLine("List of Commands: help, quit, version, look, move / go, examine.");
                        Console.WriteLine("Commands with Parameters: look <direction>, move <direction>");
                        Console.WriteLine("Quick Commands: l (look), n (north / move north), etc.");
                        break;
                    case Command.GameQuit:
                        Environment.Exit(0);
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
                        Console.WriteLine("Examine not implemented yet.");
                        break;
                    default:
                        Console.WriteLine("Unrecognized command. Type \"help\" for a list of commands.");
                        break;
                }
            }
        }
    }
}
