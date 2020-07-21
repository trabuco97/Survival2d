using System;
using System.Linq;
using UnityEngine;

using Survival2D.Systems.Statistics;
using ConsoleChat;

namespace Survival2D.Systems.HealthArmor.Command
{
    [CreateAssetMenu(fileName = "New Health Command", menuName = "Custom/Command/ModifyHealth")]
    public class ModifyHealthCommand : IConsoleCommand
    {
        /// <summary>
        /// IncrementalStat.TemporalType - DeltaValue - StatusApplied[]
        /// </summary>
        public override string CommandArgs => "<TemporalType> <int> <param string[]>";

        public override bool Process(string[] args)
        {
            if (args.Length < 2) return false;

            if (!Enum.TryParse(args[0], out IncrementalStat.TemporalType temporalType))
            {
                return false;
            }

            if (!float.TryParse(args[1], out var delta_health))
            {
                return false;
            }

            string[] status = args.Skip(2).ToArray();

            var player_health = PlayerManager.instance.player_object.GetComponentInChildren<HealthArmorSystem>();
            player_health.ModifyHealth(new HealthModificationInfo { health_delta_value = delta_health, temporal_delta_type = temporalType, status_applied = status });
            return true;
        }

        protected override TabCompletitionParser GenerateCustomTabCompletition()
        {
            var output = new TabCompletitionParser(CommandWord, 3);
            output.TryAddTabGenerator(new IncrementalTypeHealthNameGenerator(), 0);

            return output;
        }
    }
}