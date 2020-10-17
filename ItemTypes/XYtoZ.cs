using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class XYtoZ : Item
    {
        public string Y;
        public string Z;
        public Item itemY;
        public Item itemZ;
        readonly GameLoop gameLoop;
        readonly string combineMessage;

        public XYtoZ(string codeName, string name, string examineText, GameLoop gameLoop, string Y, string Z, string combineMessage) : base("XYtoZ", codeName, name, examineText)
        {
            this.Y = Y.ToLower();
            this.Z = Z.ToLower();
            this.gameLoop = gameLoop;
            this.combineMessage = combineMessage;
        }

        public override void CombineItem(List<Item> inventory, Item otherItem)
        {
            if (otherItem == itemY)
            {
                Program.PrintWrappedText("You combine " + name + " and " + otherItem.name + ". " + combineMessage);
                inventory.Add(itemZ);
                gameLoop.inactiveItems.Remove(itemZ);
                gameLoop.inactiveItems.Add(itemY);
                gameLoop.inactiveItems.Add(this);
                inventory.Remove(itemY);
                inventory.Remove(this);
            }
            else
            {
                Program.PrintWrappedText("Nothing interesting happens.");
            }
        }
    }
}
