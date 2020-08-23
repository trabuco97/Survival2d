using UnityEngine;
using NUnit.Framework;

using Survival2D.Systems.Item;
using Survival2D.Systems.Item.Crafting;

namespace Test.Item.Crafting
{
    public class CraftingTests 
    {
        [Test]
        public void Crafting_CanRecipeBeCrafted_GivenItems()
        {
            var list1 = new ItemObject[]
            {
                new ItemObject(ItemType.Material, 0, 1, false),
                new ItemObject(ItemType.Suit, 0, 2, false)
            };
            var list2 = new ItemObject[]
            {
                new ItemObject(ItemType.Suit, 0, 2, false),
                new ItemObject(ItemType.Material, 0, 1, false)
            };
            var list3 = new ItemObject[]
            {
                new ItemObject(ItemType.Suit, 1, 2, false),
                new ItemObject(ItemType.Material, 0, 1, false)
            };
            var list4 = new ItemObject[]
            {
                new ItemObject(ItemType.Material, 0, 1, false),
                new ItemObject(ItemType.Suit, 0, 1, false)
            };


            var input_crafing = new ItemCraftingData[]
            {
                new ItemCraftingData{ type = ItemType.Material, id = 0, stack_required = 1},
                new ItemCraftingData{ type = ItemType.Suit, id = 0, stack_required = 2},
            };

            var recipe = new CraftingRecipe(input_crafing, input_crafing);
            Assert.IsTrue(recipe.CanBeCrafted(list1));
            Assert.IsTrue(recipe.CanBeCrafted(list2));
            Assert.IsFalse(recipe.CanBeCrafted(list3));
            Assert.IsFalse(recipe.CanBeCrafted(list4));
        }

        [Test]
        public void Crafting_RecipePerform()
        {
            var input_crafing = new ItemCraftingData[]
            {
                new ItemCraftingData{ type = ItemType.Material, id = 0, stack_required = 2},
                new ItemCraftingData{ type = ItemType.Material, id = 1, stack_required = 2},
            };

            var output_crafting = new ItemCraftingData[]
            {
                new ItemCraftingData{ type = ItemType.Suit, id = 0, stack_required = 1},
            };

            var list1 = new ItemObject[]
            {
                new ItemObject(ItemType.Material, 0, 3, false),
                new ItemObject(ItemType.Material, 1, 2, false)
            };

            var recipe = new CraftingRecipe(input_crafing, output_crafting);
            var output = recipe.PerformRecipe(list1, out var list_result, false);


            Assert.AreEqual(ItemType.Suit, output[0].Type);
            Assert.AreEqual(1, list_result.Length);
            Assert.AreEqual(1, list_result[0].CurrentStack);

        }
    }
}