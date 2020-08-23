using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Item.Crafting
{
    public class Scriptable_CraftingRecipe : ScriptableObject
    {
        public ItemCraftingData[] input_crafting = null;
        public ItemCraftingData[] output_crafting = null;

        // TODO. make system that parses this into conditions instances
        public int conditions = 0;

    }
}