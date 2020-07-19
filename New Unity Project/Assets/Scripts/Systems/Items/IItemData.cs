using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Item
{
    /// <summary>
    /// Abstract class where all the items should inherit from
    /// </summary>
    public abstract class IItemData : ScriptableObject
    {
        [Header("Inventory data")]
        public string item_name;
        public uint max_stack;

        public bool is_stackable;
        public ItemType type;
        public ItemRarity rarity;

        public Sprite ui_display;
    }
}