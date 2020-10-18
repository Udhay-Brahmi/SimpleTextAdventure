using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class Item
    {
        public string codeName;
        public string name;
        public string examineText;
        public string type;

        public Item()
        {
            codeName = "NULL";
            name = "NULL";
            examineText = "NULL";
            type = "NULL";
        }

        public Item(string type, string codeName, string name, string examineText)
        {
            this.type = type;
            this.codeName = codeName.ToLower();
            this.name = name;
            this.examineText = examineText;
        }

        public override string ToString()
        {
            return name;
        }

        public void PrintExamineText()
        {
            Program.PrintWrappedText(examineText);
        }

        public virtual void UseItem(List<Item> inventory)
        {
            Program.PrintWrappedText("Nothing interesting happens.");
        }

        public virtual void CombineItem(List<Item> inventory, Item otherItem)
        {
            Program.PrintWrappedText("These two items don't seem to combine.");
        }
    }
}
