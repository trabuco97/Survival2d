using UnityEngine;

using ConsoleChat.UI;

namespace ConsoleChat
{
    [CreateAssetMenu(fileName = "New HelpArgs Command", menuName = "Custom/Command/HelpArgs")]
    public class HelpArgsCommand : IConsoleCommand
    {
        /// <summary>
        /// Command_name
        /// </summary>
        public override string CommandArgs => "<string>";

        public override bool Process(string[] args)
        {
            if (args.Length != 1) return false;

            var command = ConsoleDeveloperManager.instance.GetCommand(args[0]);
            if (command != null)
            {
                ChatBox.Instance.AddMessage($"Command {args[0]} accepts the args {command.CommandArgs}");
                return true;
            }


            return false;
        }

        protected override bool GetArgsGeneratedNames(string[] args, out string[] names_generated, out string word_searched)
        {
            names_generated = null;
            word_searched = null;

            switch (args.Length)
            {
                case 0:
                    word_searched = string.Empty;
                    break;
                case 1:
                    word_searched = args[0];
                    break;
                default:
                    return false;
            }

            var command_generator = new CommandNamesGenerator();
            names_generated = command_generator.ObtainCommandNameMatches(word_searched);
            return true;
        }
    }
}