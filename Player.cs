using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class Player
    {
        public string name;
        public Zone currentZone;
        //public List<Item> inventory;

        public Player(string name, Zone startingZone)
        {
            this.name = name;
            currentZone = startingZone;
        }

        public void PlayGame()
        {
            // Introduction
            // Main Loop
            //      Get Input (Command)
            //      Process Command
            //      Output Results
            // End of Program

            Console.WriteLine("Introduction text here.");

            while (true)
            {
                string input = Console.ReadLine().Trim();
                if ((input == "help") || (input == ""))
                {
                    Console.WriteLine("Known commands: help, quit, look, move");
                }
                else if (input == "quit")
                {
                    break;
                }
                else if (input == "look")
                {
                    Console.WriteLine("You look around.");
                }
                else if (input == "move")
                {
                    Console.WriteLine("You move.");
                }
                else
                {
                    Console.WriteLine("I don't recognize that command.");
                }
            }
        }
    }
}
