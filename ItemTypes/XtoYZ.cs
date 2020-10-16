using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class XtoYZ : Item
    {
        public string Y;
        public string Z;
        public Item itemY;
        public Item itemZ;
        readonly GameLoop gameLoop;
        readonly string useMessage;

        public XtoYZ(string codeName, string name, string examineText, GameLoop gameLoop, string Y, string Z, string useMessage) : base("XtoYZ", codeName, name, examineText)
        {
            this.Y = Y.ToLower();
            this.Z = Z.ToLower();
            this.gameLoop = gameLoop;
            this.useMessage = useMessage;
        }

        public override void UseItem(List<Item> inventory)
        {
            Program.PrintWrappedText("You use " + name + ". " + useMessage);
            inventory.Add(itemY);
            inventory.Add(itemZ);
            gameLoop.inactiveItems.Remove(itemY);
            gameLoop.inactiveItems.Remove(itemZ);
            gameLoop.inactiveItems.Add(this);
            inventory.Remove(this);
        }
    }
}
