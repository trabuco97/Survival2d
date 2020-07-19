using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Systems.Item.Inventory
{
    [System.Flags]
    public enum ReorderType
    {
        None =      0,
        Name =      1 << 0,
        Type =      1 << 1,
        Rarity =    1 << 2
    }

    public static class InventoryReorderManager
    {
        public static void ReorderByName(Stack<InventorySpace> spaces_stack) { }
        public static void ReorderByType(Stack<InventorySpace> spaces_stack, ItemType[] type_order) { }
        public static void ReorderByRarity(Stack<InventorySpace> spaces_stack, ItemRarity[] rarity_order) { }
    }
}