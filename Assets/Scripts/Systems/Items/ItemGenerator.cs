using UnityEngine;
using System.Collections;

using Survival2D.Systems.Item.Suit;

namespace Survival2D.Systems.Item
{
    public static class ItemGenerator
    {
        // Post:    item_generated has the data already inicialized
        public static bool GenerateItem(ItemType type, int id, out ItemObject item_generated, uint current_stack = 1)
        {
            switch (type)
            {
                case ItemType.Suit:
                    item_generated = new SuitObject();
                    break;
                default:
                    item_generated = new ItemObject();
                    break;
            }

            item_generated.current_stack = current_stack;
            item_generated.Inicialize(type, id);

            return true;
        }
    }
}