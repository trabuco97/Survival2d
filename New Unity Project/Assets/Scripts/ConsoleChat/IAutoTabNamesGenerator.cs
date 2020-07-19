using UnityEngine;
using System.Collections.Generic;

namespace ConsoleChat
{
    public abstract class IAutoTabNamesGenerator
    {
        public bool GetAutoTabNames(string word_parsed, out string[] auto_tab_names)
        {
            auto_tab_names = null;

            var types_names = GetNamesToCompare();
            List<string> matches = new List<string>();
            if (word_parsed == string.Empty)
            {
                matches = new List<string>(types_names);
            }
            else
            {
                foreach (var name in types_names)
                {
                    Debug.Log(word_parsed);
                    if (name.StartsWith(word_parsed))
                    {
                        matches.Add(name);
                    }
                }

                if (matches.Count == 0) return false;
            }

            auto_tab_names = matches.ToArray();
            return true;
        }

        protected abstract string[] GetNamesToCompare();
    }
}