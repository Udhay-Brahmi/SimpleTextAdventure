using System;
using System.Linq;

namespace SimpleTextAdventure
{
    static class Parser
    {
        public static void ParseUserInput(string userInput, out Command commandOut, out string[] parametersOut)
        {
            // Found this on the internet. TODO: Understand LINQ
            string[] terms = userInput.ToLower().Split(null).Where(x => !string.IsNullOrEmpty(x)).ToArray();

            Command command;
            if (terms == null || terms.Length == 0) terms = new string[] { "" };
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
                case "north":
                case "e":
                case "east":
                case "s":
                case "south":
                case "w":
                case "west":
                    commandOut = Command.Move;
                    parametersOut = new string[] { terms[0] };
                    return;
                case "examine":
                case "study":
                    command = Command.Examine;
                    break;
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
    }
}
