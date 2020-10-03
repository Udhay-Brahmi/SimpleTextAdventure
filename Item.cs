using System;

namespace SimpleTextAdventure
{
    class Item
    {
        public string longName;
        public string shortName;

        public Item(string longName, string shortName)
        {
            this.longName = longName;
            this.shortName = shortName;
        }
    }
}
