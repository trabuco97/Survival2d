using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Item
{
    public static class ItemSystemContainerChecker
    {
        public static bool ItMatches(ItemType type, SystemType system)
        {
            bool output = false;
            switch (type)
            {
                case ItemType.Suit:
                    output = system.HasFlag(SystemType.Inventory) || system.HasFlag(SystemType.Equipment);
                    break;
                case ItemType.Material:
                    output = system.HasFlag(SystemType.Inventory);
                    break;
            }

            return output;
        }

    }
}