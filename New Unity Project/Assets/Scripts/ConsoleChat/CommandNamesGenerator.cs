using UnityEngine;
using System.Collections.Generic;

namespace ConsoleChat.UI
{
    public class CommandNamesGenerator 
    {
        private ConsoleDeveloperManager console = null;

        public CommandNamesGenerator()
        {
            console = ConsoleDeveloperManager.instance;
        }

        public bool GetAutoTabNames(string input_text, out string[] auto_tab_names, out string name_searched)
        {
            if (console.IsCommandValid(input_text))
            {
                console.ParseCommand(input_text, out var command_name, out var args);

                if (args.Length == 0)
                {
                    auto_tab_names = ObtainCommandNameMatches(command_name);
                    name_searched = command_name;
                    return auto_tab_names.Length > 0;
                }
            }

            name_searched = string.Empty;
            auto_tab_names = null;
            return false;
        }


        public string[] ObtainCommandNameMatches(string command_incomplete)
        {
            var command_names = console.GetCommandNames();
            List<string> matches = new List<string>();

            if (command_incomplete == string.Empty)
            {
                matches = new List<string>(command_names);
            }
            else
            {
                foreach (var name in command_names)
                {
                    if (name.StartsWith(command_incomplete))
                    {
                        matches.Add(name);
                    }
                }
            }

            return matches.ToArray();
        }
    }
}