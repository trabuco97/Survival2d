using UnityEngine;
using NUnit.Framework;

using Survival2D.Systems.Item;

namespace Test.Item
{
    public class ItemTests
    {
        // pre:
        //      (suit ,0) (material, 1) ( material 0)
        // post:
        //      (material, 0) (material, 1) (suit 0)
        [Test]
        public void Item_ReorderByDataAndId()
        {
            var item_array = new ItemObject[]
            {
                new ItemObject(ItemType.Suit, 0, 0, false),
                new ItemObject(ItemType.Material, 1, 0, false),
                new ItemObject(ItemType.Material, 0, 0, false),
            };


            item_array = ItemListReorder.ReorderItems(item_array);

            Assert.AreEqual(ItemType.Material, item_array[0].Type);
            Assert.AreEqual(ItemType.Material, item_array[1].Type);
            Assert.AreEqual(ItemType.Suit, item_array[2].Type);

            Assert.AreEqual(0, item_array[0].ID);
            Assert.AreEqual(1, item_array[1].ID);
            Assert.AreEqual(0, item_array[2].ID);
        }
    }
}