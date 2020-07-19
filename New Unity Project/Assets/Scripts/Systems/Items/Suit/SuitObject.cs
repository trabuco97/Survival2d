using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survival2D.Systems.Item;
using Survival2D.Systems.Item.Equipment;

namespace Survival2D.Systems.Item.Suit
{
    [System.Serializable]
    public class SuitObject : ItemObject
    {
        private SuitData suit_data = null;
        public SuitData SuitData 
        {
            get
            {
                if (suit_data == null)
                {
                    suit_data = ItemData as SuitData;
                }

                return suit_data;
            }
        }

        public float actual_rating = 0;

        public override void Inicialize(ItemType type, int id)
        {
            base.Inicialize(type, id);

            var suit_data = ItemData as SuitData;

            if (suit_data != null)
            {
                actual_rating = suit_data.base_armor_rating;
            }
        }

    }
}