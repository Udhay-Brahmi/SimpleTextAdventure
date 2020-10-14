using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class Player
    {
        public string name;
        public Zone currentZone;
        public List<Item> inventory = new List<Item>();

        public Player(string name, Zone startingZone)
        {
            this.name = name;
            currentZone = startingZone;
            currentZone.playerHasVisited = true;
        }

        public void PrintInventory()
        {
            if (inventory.Count == 0)
            {
                Program.PrintWrappedText("You are carrying nothing.");
            }
            else if (inventory.Count == 1)
            {
                Program.PrintWrappedText("You are carrying 1 item: " + string.Join(", ", inventory));
            }
            else
            {
                Program.PrintWrappedText("You are carrying " + inventory.Count + " items: " + string.Join(", ", inventory));
            }
        }

        public void LookAction(Direction direction)
        {
            currentZone.PrintLook(direction);
        }

        public void MoveAction(Direction direction)
        {
            switch (direction)
            {
                case Direction.Invalid:
                    Program.PrintWrappedText("Not a valid direction.");
                    break;
                case Direction.Here:
                    Program.PrintWrappedText("You must specify a direction to move.");
                    break;
                default:
                    if (currentZone.exits.ContainsKey(direction))
                    {
                        Program.PrintWrappedText("You move " + direction + ". ");
                        currentZone = currentZone.exits[direction];
                        Program.PrintWrappedText("You arrive at " + currentZone.name + ".");
                        if (!currentZone.playerHasVisited)
                        {
                            currentZone.PrintExamineText();
                            currentZone.playerHasVisited = true;
                        }
                    }
                    else
                    {
                        Program.PrintWrappedText("You can't move that way.");
                    }
                    break;
            }
        }

        public void ExamineAction(Parameter[] parameters)
        {
            if (parameters.Length == 0)
            {
                Program.PrintWrappedText("This command requires a target.");
            }
            else
            {
                bool targetFound = false;
                if (currentZone.items.Count > 0)
                {
                    foreach (Item item in currentZone.items)
                    {
                        if (item.codeName == parameters[0].stringParameter)
                        {
                            item.PrintExamineText();
                            targetFound = true;
                        }
                    }
                }
                if (inventory.Count > 0)
                {
                    foreach (Item item in inventory)
                    {
                        if (item.codeName == parameters[0].stringParameter)
                        {
                            item.PrintExamineText();
                            targetFound = true;
                        }
                    }
                }
                if (currentZone.codeName == parameters[0].stringParameter)
                {
                    Program.PrintWrappedText("You examine " + currentZone.name + ".");
                    currentZone.PrintExamineText();
                    targetFound = true;
                }
                foreach (KeyValuePair<Direction, Zone> exit in currentZone.exits)
                {
                    if (exit.Value.codeName == parameters[0].stringParameter)
                    {
                        Program.PrintWrappedText("You are too far away to examine " + exit.Value.name + ".");
                        targetFound = true;
                    }
                }
                if (!targetFound)
                {
                    Program.PrintWrappedText("Unrecognized target.");
                }
            }
        }

        bool TryFindTarget(Parameter[] parameters, out Parameter result)
        {
            if (parameters == null || parameters.Length == 0 || parameters[0].type != ParameterType.String)
            {
                result = new Parameter("");
                return false;
            }

            if (parameters[0].stringParameter == "all")
            {
                result = new Parameter("all");
                return true;
            }

            if (parameters[0].stringParameter == currentZone.name)
            {
                result = new Parameter(currentZone);
                return true;
            }

            foreach (KeyValuePair<Direction, Zone> exit in currentZone.exits)
            {
                if (exit.Value.codeName == parameters[0].stringParameter)
                {
                    result = new Parameter(currentZone.exits[exit.Key]);
                    return true;
                }
            }

            foreach (Item item in inventory)
            {
                if (item.codeName == parameters[0].stringParameter)
                {
                    result = new Parameter(item);
                    return true;
                }
            }

            foreach (Item item in currentZone.items)
            {
                if (item.codeName == parameters[0].stringParameter)
                {
                    result = new Parameter(item);
                    return true;
                }
            }

            result = parameters[0];
            return false;
        }

        List<Item> LocateItem(Item item)
        {
            if (inventory.Contains(item))
            {
                return inventory;
            }
            else if (currentZone.items.Contains(item))
            {
                return currentZone.items;
            }
            else return null;
        }

        public void TakeAction(Parameter[] parameters)
        {
            if (TryFindTarget(parameters, out Parameter target) && target.type == ParameterType.Item)
            {
                if (LocateItem(target.itemParameter) == currentZone.items)
                {
                    inventory.Add(target.itemParameter);
                    currentZone.items.Remove(target.itemParameter);
                    Program.PrintWrappedText("You take " + target.itemParameter.name + ".");
                }
                else
                {
                    Program.PrintWrappedText("That item is not somewhere you can take it.");
                }
            }
            else if (target.stringParameter == "all")
            {
                if (currentZone.items.Count > 0)
                {
                    while (currentZone.items.Count > 0)
                    {
                        inventory.Add(currentZone.items[0]);
                        currentZone.items.Remove(currentZone.items[0]);
                    }
                    Program.PrintWrappedText("You take all the items in this area.");
                }
                else
                {
                    Program.PrintWrappedText("There are no items to take.");
                }
            }
            else
            {
                Program.PrintWrappedText("Not a valid target.");
            }
        }

        public void DropAction(Parameter[] parameters)
        {
            if (TryFindTarget(parameters, out Parameter target) && target.type == ParameterType.Item)
            {
                if (LocateItem(target.itemParameter) == inventory)
                {
                    currentZone.items.Add(target.itemParameter);
                    inventory.Remove(target.itemParameter);
                    Program.PrintWrappedText("You drop " + target.itemParameter.name + ".");
                }
                else
                {
                    Program.PrintWrappedText("You aren't carrying that item.");
                }
            }
            else if (target.stringParameter == "all")
            {
                if (inventory.Count > 0)
                {
                    while (inventory.Count > 0)
                    {
                        currentZone.items.Add(inventory[0]);
                        inventory.Remove(inventory[0]);
                    }
                    Program.PrintWrappedText("You drop all the items you were carrying.");
                }
                else
                {
                    Program.PrintWrappedText("You are carrying no items.");
                }
            }
            else
            {
                Program.PrintWrappedText("Not a valid target.");
            }
        }

        public void UseAction(Parameter[] parameters)
        {
            if (TryFindTarget(parameters, out Parameter target) && target.type == ParameterType.Item)
            {
                if (LocateItem(target.itemParameter) == inventory)
                {
                    target.itemParameter.UseItem(inventory);
                }
                else
                {
                    Program.PrintWrappedText("You aren't carrying that item.");
                }
            }
            else
            {
                Program.PrintWrappedText("Not a valid target.");
            }
        }
    }
}
