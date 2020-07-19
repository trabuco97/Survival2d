using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConsoleChat
{
    public class CommandData
    {
        public string commnad_name;
        public string[] command_args;
    }

    public class ConsoleDeveloperManager : MonoBehaviour
    {
        public enum CommandProcess { Success, Error_Name, Error_Args }

        public class CommandProcessInfo
        {
            public CommandProcess result;
            public string message;

        }

        [SerializeField] private string command_prefix = string.Empty;
        [SerializeField] private List<IConsoleCommand> console_commands = new List<IConsoleCommand>();

        private const string COMMAND_EXEC = "/ EXECUTED {0} command";
        private const string ERROR_NO_COMMAND = "/ ERROR: Command name {0} not found";
        private const string ERROR_NO_CORRECT_ARGS = "/ ERROR: Command name {0} has inapropiate args ({1})";

        public static ConsoleDeveloperManager instance = null;
        public string CommandPrefix { get { return command_prefix; } }
        public List<IConsoleCommand> Commands { get { return console_commands; } }

        private void Awake()
        {
            instance = this;
        }


        public bool IsCommandValid(string input_command)
        {
            return input_command.StartsWith(command_prefix);
        }

        // Pre :    IsCommandValid is called beforehand
        public CommandProcessInfo ProcessCommand(string input_command)
        {
            var output = new CommandProcessInfo();
            output.result = CommandProcess.Error_Name;

            if (ParseCommand(input_command, out var command_name, out var args))
            {
                output.message = string.Format(ERROR_NO_COMMAND, command_name);

                foreach (var command in console_commands)
                {
                    if (!command_name.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (command.Process(args))
                    {
                        output.result = CommandProcess.Success;
                        output.message = string.Format(COMMAND_EXEC, command_name);

                    }
                    else
                    {
                        output.result = CommandProcess.Error_Args;
                        output.message = string.Format(ERROR_NO_CORRECT_ARGS, command_name, command.CommandArgs); ;

                    }
                    break;
                }
            }

            return output;
        }

        public IConsoleCommand GetCommand(string command_name)
        {
            foreach (var command in console_commands)
            {
                if (command_name == command.CommandWord)
                    return command;
            }

            return null;
        }

        public string[] GetCommandNames()
        {
            List<string> output_list = new List<string>();

            foreach (var command in console_commands)
            {
                output_list.Add(command.CommandWord);
            }

            return output_list.ToArray();
        }

        public bool ParseCommand(string input_command, out string command_name, out string[] args)
        {
            args = null;
            command_name = string.Empty;
            if (input_command == string.Empty) return false;

            input_command = input_command.Remove(0, command_prefix.Length);

            string[] command_split = input_command.Split(' ');
            command_name = command_split[0];
            args = command_split.Skip(1).ToArray();

            return true;
        }
        
    }
}