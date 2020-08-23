#if UNITY_EDITOR
using UnityEngine;
#endif

using System.Collections.Generic;

namespace Survival2D.Systems.Item.Crafting
{
    public class CraftingRecipe
    {
        private List<ItemCraftingData> input_crafting_data_container;
        private List<ItemCraftingData> ouput_crafting_data_container;

        public CraftingRecipe(ItemCraftingData[] input, ItemCraftingData[] output)
        {
            input = ReorderCraftingData(input);
            output = ReorderCraftingData(output);


            input_crafting_data_container = new List<ItemCraftingData>(input);
            ouput_crafting_data_container = new List<ItemCraftingData>(output);
        }


        public bool CanBeCrafted(ItemObject[] items_toUse)
        {
            if (input_crafting_data_container.Count != items_toUse.Length) return false;
            var new_items = ItemListReorder.ReorderItems(items_toUse);


            for (int i = 0; i < items_toUse.Length; i++)
            {
                var crafting_data = input_crafting_data_container[i];
                var item = new_items[i];

                bool condition =
                    crafting_data.type == item.Type &&
                    crafting_data.id == item.ID &&
                    crafting_data.stack_required <= item.CurrentStack;

                if (!condition) return false;
            }

            return true;
        }

        // pre:     canbecrafted was called beforehand and returned true
        // post:    return the output as itemobjects
        //          inputresult gives the remaining object, in order
        public ItemObject[] PerformRecipe(ItemObject[] input, out ItemObject[] input_result, bool are_inicialized = true)
        {
            var input_reordered = ItemListReorder.ReorderItems(input);
            var input_result_list = new List<ItemObject>();

            
            for (int i = 0; i < input.Length; i++)
            {
                var item_object = input_reordered[i];
                var item_crafting_data = input_crafting_data_container[i];
                if (item_object.CurrentStack - item_crafting_data.stack_required > 0)
                {
                    item_object.CurrentStack -= item_crafting_data.stack_required;
                    input_result_list.Add(item_object);
                }
            }

            input_result = input_result_list.ToArray();
            var output = new ItemObject[ouput_crafting_data_container.Count];
            for (int i = 0; i < output.Length; i++)
            {
                var output_crafting_data = ouput_crafting_data_container[i];
                if (ItemGenerator.GenerateItem(output_crafting_data.type, output_crafting_data.id, out var item, output_crafting_data.stack_required, are_inicialized))
                {
                    output[i] = item;
                }
#if UNITY_EDITOR
                else
                {
                    Debug.Log("error in item generator when creating item in the crafting recipe");
                }
#endif
            }

            return output;
        }

        private static ItemCraftingData[] ReorderCraftingData(ItemCraftingData[] items_toReorder)
        {
            var list = new List<ItemCraftingData>(items_toReorder);
            list.Sort((ItemCraftingData a, ItemCraftingData b) =>
            {
                if ((int)a.type < (int)b.type)
                {
                    return -1;
                }
                else if ((int)a.type > (int)b.type)
                {
                    return 1;

                }
                else
                {
                    if (a.id < b.id)
                        return -1;
                    else if (a.id > b.id)
                        return 1;
                    else
                        return 0;
                }
            });

            return list.ToArray();

        }

    }
}