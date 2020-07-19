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

        public string CommandWord { get { return command_name; } }
        public abstract string CommandArgs { get; }

        /// <summary>
        /// 
        ///  Ojo:   The function takes care if the length of args and 
        ///         the parse og each one
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract bool Process(string[] args);
        
        public bool CheckAutoTabName(string input_text, out string[] names_generated, out string word_searched)
        {
            names_generated = null;
            word_searched = string.Empty;

            var console = ConsoleDeveloperManager.instance;
            if (console.ParseCommand(input_text, out var command_name, out var args) && command_name == this.CommandWord)
            {
                if (GetArgsGeneratedNames(args, out var names, out var word))
                {
                    names_generated = names;
                    word_searched = word;
                    return true;
                }
            }

            return false;
        }


        protected abstract bool GetArgsGeneratedNames(string[] args, out string[] names_generated, out string word_searched);
    }
}