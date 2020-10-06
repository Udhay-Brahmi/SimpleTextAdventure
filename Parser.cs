﻿using System;
using System.Linq;

namespace SimpleTextAdventure
{
    static class Parser
    {
        public static void ParseUserInput(string userInput, out Command commandOut, out Parameter[] parametersOut)
        {
            // Found this on the internet. TODO: Understand LINQ
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
                    TryParseDirection(terms[0], out Direction direction);
                    parametersOut = new Parameter[] { new Parameter(direction) };
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
