
using ConsoleChat;
using Survival2D.Systems.Statistics;

namespace Survival2D.Systems.HealthArmor.Command
{
    public class IncrementalTypeHealthNameGenerator : ITabNamesGenerator
    {
        protected override string[] GetNamesToCompare()
        {
            int max_size = (int)IncrementalStat.TemporalType.MAX_TYPES;
            string[] output = new string[max_size];
            for (int i = 0; i < max_size; i++)
            {
                output[i] = ((IncrementalStat.TemporalType)i).ToString();
            }

            return output;
        }
    }
}