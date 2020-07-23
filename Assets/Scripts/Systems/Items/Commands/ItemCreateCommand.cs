using System;
using UnityEngine;

using ConsoleChat;
using Survival2D.Systems.Item.Inventory;
using Survival2D.Entities.Player;

namespace Survival2D.Systems.Item.Command
{
    [CreateAssetMenu(fileName = "New ItemCreate Command", menuName = "Custom/Command/ItemCreate")]
    public class ItemCreateCommand : IConsoleCommand
    {
        /// <summary>
        /// ItemType - Id - CurrentStack
        /// </summary>
        public override string CommandArgs => "<ItemType> <int> <uint>";

        public override bool Process(string[] args)
        {
            if (args.Length != 3) return false;

            if (Enum.TryParse(args[0], out ItemType item_type) &&
                int.TryParse(args[1], out var id) &&
                uint.TryParse(args[2], out var current_stack))
            {

                if (ItemGenerator.GenerateItem(item_type, id, out var item_object, current_stack))
                {
                    var inventory = PlayerManager.Instance.player_object.GetComponentInChildren<InventorySystem>();
                    inventory.AddItemToAvailable(item_object);

                    return true;
                }

            }

            return false;
        }

        protected override TabCompletitionParser GenerateCustomTabCompletition()
        {
            var output = new TabCompletitionParser(CommandWord, 3);
            output.TryAddTabGenerator(new ItemTypeNameGenerator(), 0);

            return output;
        }
    }

}