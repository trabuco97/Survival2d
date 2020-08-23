using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Systems.Item.Crafting
{
    public class CraftingRecipeDatabase
    {
        private List<CraftingRecipe> recipes_container;

        public CraftingRecipeDatabase(CraftingRecipe[] recipes_list)
        {
            recipes_container = new List<CraftingRecipe>(recipes_container);
        }

        public CraftingRecipe[] GetCompatibleRecipes(ItemObject[] input)
        {
            var recipes_list = new List<CraftingRecipe>();

            foreach (var recipe in recipes_container)
            {
                if (recipe.CanBeCrafted(input))
                {
                    recipes_list.Add(recipe);
                }
            }

            return recipes_list.ToArray();
        }
    }
}