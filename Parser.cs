using System;
using System.Linq;

namespace SimpleTextAdventure
{
    static class Parser
    {
        public static void ParseUserInput(string userInput, out Command commandOut, out Parameter[] parametersOut)
        {
            string[] terms = userInput.ToLower().Split(null).Where(x => !string.IsNullOrEmpty(x)).ToArray();

            Command command;
            if (terms == null || terms.Length == 0) terms = new string[] { "" };
            switch (terms[0])
            {
                case "quit":
                    command = Command.GameQuit;
                    break;
                case "":
                case "help":
                case "?":
                case "commands":
                    command = Command.GameHelp;
                    break;
                case "version":
                case "about":
                case "info":
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
                case "u":
                case "up":
                case "d":
                case "down":
                case "in":
                case "inside":
                case "inward":
                case "out":
                case "outside":
                case "outward":
                    commandOut = Command.Move;
                    TryParseDirection(terms[0], out Direction direction);
                    parametersOut = new Parameter[] { new Parameter(direction) };
                    return;
                case "examine":
                case "x":
                case "study":
                    command = Command.Examine;
                    break;
                case "wait":
                    command = Command.Wait;
                    break;
                case "inventory":
                case "inv":
                case "i":
                    command = Command.Inventory;
                    break;
                case "take":
                case "get":
                case "grab":
                    command = Command.Take;
                    break;
                case "drop":
                case "put":
                case "set":
                    command = Command.Drop;
                    break;
                case "use":
                    command = Command.Use;
                    break;
                case "combine":
                    command = Command.Combine;
                    break;
                default:
                    command = Command.Invalid;
                    break;
            }
            commandOut = command;

            if (command == Command.Invalid)
            {
                parametersOut = new Parameter[0];
                return;
            }

            Parameter[] parameters = new Parameter[terms.Length - 1];
            if (parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (TryParseDirection(terms[i + 1], out Direction directionParameter))
                    {
                        parameters[i] = new Parameter(directionParameter);
                    }
                    else
                    {
                        parameters[i] = new Parameter(terms[i + 1]);
                    }
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
                case "u":
                case "up":
                    direction = Direction.Up;
                    break;
                case "d":
                case "down":
                    direction = Direction.Down;
                    break;
                case "in":
                case "inside":
                case "inward":
                    direction = Direction.In;
                    break;
                case "out":
                case "outside":
                case "outward":
                    direction = Direction.Out;
                    break;
                default:
                    isValidDirection = false;
                    direction = Direction.Invalid;
                    break;
            }
            directionOut = direction;
            return isValidDirection;
        }

        public static Direction ParseDirectionParameter(Parameter[] parameters)
        {
            if (parameters.Length == 0)
            {
                return Direction.Here;
            }
            else if (parameters[0].type == ParameterType.Direction)
            {
                return parameters[0].directionParameter;
            }
            else
            {
                return Direction.Invalid;
            }
        }
    }
}
