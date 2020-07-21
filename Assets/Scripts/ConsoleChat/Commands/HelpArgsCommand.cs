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

            var command = ConsoleBehaviour.Instance.Console.GetCommand(args[0]);
            if (command != null)
            {
                UI_ChatQueue.Instance.AddMessage($"Command {args[0]} accepts the args {command.CommandArgs}");
                return true;
            }


            return false;
        }

        protected override TabCompletitionParser GenerateCustomTabCompletition()
        {
            var output = new TabCompletitionParser(CommandWord, 1);
            output.TryAddTabGenerator(new CommandNamesGenerator(ConsoleBehaviour.Instance.Console), 0);

            return output;
        }
    }
}