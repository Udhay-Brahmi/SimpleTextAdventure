using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class Item
    {
        public string codeName;
        public string name;
        public string examineText;

        public Item(string codeName, string name, string examineText)
        {
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
    }
}
