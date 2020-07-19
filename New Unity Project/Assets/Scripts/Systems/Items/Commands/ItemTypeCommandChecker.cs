using UnityEngine;
using System.Collections;

using ConsoleChat;

namespace Survival2D.Systems.Item.Command
{
    public class ItemTypeCommandChecker : IAutoTabNamesGenerator
    {
        protected override string[] GetNamesToCompare()
        {
            int max_size = (int)ItemType.MAX_TYPES;
            string[] output = new string[max_size];

            for (int i = 0; i < max_size; i++)
            {
                output[i] = ((ItemType)i).ToString();
            }

            return output;
        }
    }
}