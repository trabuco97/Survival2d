using UnityEngine;
using System.Collections;

using Survival2D.Systems.Item.Suit;

namespace Survival2D.Systems.Item
{
    public static class ItemGenerator
    {
        public static bool GenerateItem(out ItemObject item_generated, ItemType type, int id, uint current_stack = 1)
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