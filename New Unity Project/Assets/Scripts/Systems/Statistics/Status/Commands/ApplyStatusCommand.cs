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

            var names_checker = new StatusNameCommandChecker();
            if (names_checker.GetAutoTabNames(word_searched, out var names))
            {
                names_generated = names;
                return true;
            }


            return false;
        }
    }
}