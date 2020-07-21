using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleChat
{
    public class CommandData
    {
        public string commnad_name;
        public string[] command_args;
    }


    public class Console
    {
        private const string COMMAND_EXEC = "/ EXECUTED {0} command";
        private const string ERROR_NO_COMMAND = "/ ERROR: Command name {0} not found";
        private const string ERROR_NO_CORRECT_ARGS = "/ ERROR: Command name {0} has inapropiate args ({1})";

        public enum CommandProcess { Success, Error_Name, Error_Args }

        public class CommandProcessInfo
        {
            public CommandProcess result;
            public string message;
        }

        private string command_prefix = string.Empty;
        private List<IConsoleCommand> commands_holder = null;

        public Console(string command_prefix, IEnumerable<IConsoleCommand> commands)
        {
            this.command_prefix = command_prefix;
            this.commands_holder = new List<IConsoleCommand>(commands);
        }


        public bool IsCommandValid(string input_command)
        {
            return input_command.StartsWith(command_prefix);
        }

        // Pre :    IsCommandValid is called beforehand
        public CommandProcessInfo ProcessCommand(string input_command)
        {
            var output = new CommandProcessInfo();
            output.result = CommandProcess.Error_Args;
            output.message = ERROR_NO_COMMAND;                  // placeholder, replace beacuse repeated code

            if (ParseCommand(input_command, out var command_name, out var args))
            {
                return ProcessCommand(command_name, args);
            }

            return output;
        }

        public CommandProcessInfo ProcessCommand(string command_name, string[] args)
        {
            var output = new CommandProcessInfo();
            output.result = CommandProcess.Error_Args;
            output.message = string.Format(ERROR_NO_COMMAND, command_name);


            foreach (var command in commands_holder)
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
                    output.message = string.Format(ERROR_NO_CORRECT_ARGS, command_name, command.CommandArgs); 
                }


                break;
            }

            return output;
        }

        public IConsoleCommand GetCommand(string command_name)
        {
            foreach (var command in commands_holder)
            {
                if (command_name == command.CommandWord)
                    return command;
            }

            return null;
        }

        public IConsoleCommand[] GetCommandArray()
        {
            return commands_holder.ToArray();
        }

        public string[] GetCommandNames()
        {
            List<string> output_list = new List<string>();
            foreach (var command in commands_holder)
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