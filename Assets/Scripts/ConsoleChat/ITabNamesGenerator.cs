using UnityEngine;
using System.Collections.Generic;

namespace ConsoleChat
{
    public abstract class ITabNamesGenerator
    {
        // Get all the names that start with the incomplete name
        // Post:    return false if, no name matches with the name to replace or
        //          the name to replace exist in the completition names
        public bool GetTabNames(string name_to_replace, out string[] completition_names)
        {
            completition_names = null;

            var types_names = GetNamesToCompare();
            List<string> matches = new List<string>();
            if (name_to_replace == string.Empty)
            {
                matches = new List<string>(types_names);
            }
            else
            {
                foreach (var name in types_names)
                {
                    if (name == name_to_replace) return false;

                    if (name.StartsWith(name_to_replace))
                    {
                        matches.Add(name);
                    }
                }

                if (matches.Count == 0) return false;
            }

            completition_names = matches.ToArray();
            return true;
        }

        /// <summary>
        /// Returns the all the names that could be shown in the tab completition
        /// </summary>
        /// <returns></returns>
        protected abstract string[] GetNamesToCompare();
    }
}