using System;

namespace Survival2D.Systems.Item.Crafting
{
    [Serializable]
    public struct ItemCraftingData
    {
        public ItemType type;
        public int id;
        public uint stack_required;
    }
}