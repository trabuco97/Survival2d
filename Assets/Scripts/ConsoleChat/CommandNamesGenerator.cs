using UnityEngine;
using System.Collections.Generic;

namespace ConsoleChat
{
    // This concrete implementation is used by the ui_logic itself to determine if a name of command has beend
    public class CommandNamesGenerator : ITabNamesGenerator
    {
        private Console console;

        public CommandNamesGenerator(Console console)
        {
            this.console = console;
        }

        protected override string[] GetNamesToCompare()
        {
            return console.GetCommandNames();
        }
    }
}