using System;
using UnityEngine;

using ConsoleChat;
using Survival2D.Systems.Item.Inventory;

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

                if (ItemGenerator.GenerateItem(out var item_object, item_type, id, current_stack))
                {
                    var inventory = PlayerManager.instance.player_object.GetComponentInChildren<InventorySystem>();
                    inventory.AddItemToAvailable(item_object);

                    return true;
                }

            }

            return false;
        }

        protected override bool GetArgsGeneratedNames(string[] args, out string[] names_generated, out string word_searched)
        {
            names_generated = null;
            word_searched = string.Empty;

            var item_type_checker = new ItemTypeCommandChecker();
            switch (args.Length)
            {
                case 0:
                    word_searched = string.Empty;
                    break;
                case 1:
                    word_searched = args[0];
                    break;
                default:
                    return false;
            }

            if (item_type_checker.GetAutoTabNames(word_searched, out var names))
            {
                names_generated = names;
                return true;
            }


            return false;
        }
    }

}