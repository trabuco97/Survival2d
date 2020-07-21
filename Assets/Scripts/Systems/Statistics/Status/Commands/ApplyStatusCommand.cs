using UnityEngine;
using System.Collections;

using ConsoleChat;

namespace Survival2D.Systems.Statistics.Status.Command
{
    [CreateAssetMenu(fileName = "New ApplyStatus Command", menuName = "Custom/Command/ApplyStatus")]
    public class ApplyStatusCommand : IConsoleCommand
    {
        /// <summary>
        /// Status_name
        /// </summary>
        public override string CommandArgs => "<string>";

        public override bool Process(string[] args)
        {
            if (args.Length != 1) return false;

            var status_system = PlayerManager.instance.player_object.GetComponentInChildren<StatusSystem>();
            if (status_system != null)
            {
                status_system.AddStatus(args[0]);
                return true;
            }


            return false;

        }

        protected override TabCompletitionParser GenerateCustomTabCompletition()
        {
            var output = new TabCompletitionParser(CommandWord, 1);
            output.TryAddTabGenerator(new StatusNameGenerator(), 0);

            return output;
        }
    }
}