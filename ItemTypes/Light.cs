using System;
using System.Collections.Generic;

namespace SimpleTextAdventure
{
    class Light : Item
    {
        readonly string activateMessage;
        readonly string deactivateMessage;
        readonly string inactiveName;
        readonly string activeName;
        readonly string inactiveExamineText;
        readonly string activeExamineText;
        public bool isActive;

        public Light(string codeName, string inactiveName, string activeName, string examineText, string activateMessage, string deactivateMessage) : base("Light", codeName, inactiveName, examineText)
        {
            this.activateMessage = activateMessage;
            this.deactivateMessage = deactivateMessage;
            this.inactiveName = inactiveName;
            this.activeName = activeName;
            inactiveExamineText = examineText.Substring(0, examineText.IndexOf('|'));
            activeExamineText = examineText.Substring(examineText.IndexOf('|') + 1);
            base.examineText = inactiveExamineText;
        }

        public override void UseItem(List<Item> inventory)
        {
            if (isActive)
            {
                Program.PrintWrappedText("You use " + name + ". " + deactivateMessage);
                name = inactiveName;
                examineText = inactiveExamineText;
                isActive = false;
            }
            else
            {
                Program.PrintWrappedText("You use " + name + ". " + activateMessage);
                name = activeName;
                examineText = activeExamineText;
                isActive = true;
            }
        }
    }
}
