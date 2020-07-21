using System.Collections.Generic;
using UnityEngine;
using ConsoleChat.UI;

namespace ConsoleChat
{
    /// <summary>
    /// Each command access and modifies data by accesing singletons, probably
    /// </summary>
    public abstract class IConsoleCommand : ScriptableObject
    {
        [SerializeField] private string command_name = string.Empty;

        private TabCompletitionParser tab_completition = null;
        public TabCompletitionParser TabCompletition 
        {
            get 
            {
                if (tab_completition == null)
                {
                    tab_completition = GenerateCustomTabCompletition();
                }

                return tab_completition;
            } 
        }

        public string CommandWord { get { return command_name; } }
        public abstract string CommandArgs { get; }

        /// <summary>
        ///  Ojo:   The function takes care if the length of args and 
        ///         the parse og each one
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract bool Process(string[] args);

        protected abstract TabCompletitionParser GenerateCustomTabCompletition();
    }
}