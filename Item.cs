using System;

namespace SimpleTextAdventure
{
    class Item
    {
        public string referenceName;
        public string briefDescription;
        public string examineText;

        public Item(string referenceName, string briefDescription, string examineText)
        {
            this.referenceName = referenceName;
            this.briefDescription = briefDescription;
            this.examineText = examineText;
        }

        public string GetExamineText()
        {
            return examineText;
        }
    }
}
