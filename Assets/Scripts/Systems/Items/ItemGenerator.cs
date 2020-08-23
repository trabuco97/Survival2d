using UnityEngine;
using System.Collections;

using Survival2D.Systems.Item.Suit;
using Survival2D.Systems.Item.Harpoon;

namespace Survival2D.Systems.Item
{
    public static class ItemGenerator
    {
        // Post:    item_generated has the data already inicialized
        public static bool GenerateItem(ItemType type, int id, out ItemObject item_generated, uint current_stack = 1, bool inicialized_data = true)
        {
            switch (type)
            {
                case ItemType.Suit:
                    item_generated = new SuitObject(type, id, current_stack, inicialized_data);
                    break;
                case ItemType.Harpoon:
                    item_generated = new HarpoonObject(type, id, current_stack, inicialized_data);
                    break;
                default:
                    item_generated = new ItemObject(type, id, current_stack, inicialized_data);
                    break;
            }

            return true;
        }
    }
}