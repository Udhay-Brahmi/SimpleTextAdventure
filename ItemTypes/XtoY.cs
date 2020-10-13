using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class XtoY : Item
    {
        public string Y;
        public Item itemY;
        readonly GameLoop gameLoop;
        readonly string useMessage;

        public XtoY(string codeName, string name, string examineText, GameLoop gameLoop, string Y, string useMessage) : base(codeName, name, examineText)
        {
            this.Y = Y.ToLower();
            this.gameLoop = gameLoop;
            this.useMessage = useMessage;
        }

        public override void UseItem(List<Item> inventory)
        {
            Program.PrintWrappedText("You use " + name + ". " + useMessage);
            inventory.Add(itemY);
            gameLoop.inactiveItems.Remove(itemY);
            gameLoop.inactiveItems.Add(this);
            inventory.Remove(this);
        }
    }
}
