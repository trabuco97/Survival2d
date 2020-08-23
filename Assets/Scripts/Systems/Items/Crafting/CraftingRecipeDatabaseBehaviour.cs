using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Item.Crafting
{
    public class CraftingRecipeDatabaseBehaviour : MonoBehaviour
    {
        [SerializeField] private Scriptable_CraftingRecipe[] scriptable_recipes = null;

        public bool IsInicialized { get; private set; }
        public CraftingRecipeDatabase Database { get; private set; }

        private void Awake()
        {
            IsInicialized = scriptable_recipes != null || scriptable_recipes.Length == 0;


            if (IsInicialized)
            {
                Database = new CraftingRecipeDatabase(GetCraftingRecipes());
            }
        }





        private CraftingRecipe[] GetCraftingRecipes()
        {
            var output = new CraftingRecipe[scriptable_recipes.Length];
            for (int i = 0; i < output.Length; i++)
            {
                var recipe = new CraftingRecipe(scriptable_recipes[i].input_crafting, scriptable_recipes[i].output_crafting);
                output[i] = recipe;
            }

            return output;
        }
    }
}