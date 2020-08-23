using System;

using Survival2D.Systems.Item.Equipment;


namespace Survival2D.Systems.Item.Suit
{
    [Serializable]
    public class SuitObject : ItemObject, IEquipableObjWithStatus
    {
        private SuitData suit_data = null;
        private float actual_rating = 0;

        public float ActualRating
        {
            get { return actual_rating; }
            set
            {
                if (actual_rating > suit_data.base_armor_rating)
                {
                    actual_rating = suit_data.base_armor_rating;
                }
                else if (actual_rating < 0)
                {
                    actual_rating = 0;
                }
                else
                {
                    actual_rating = value;
                }
            }
        }
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

        public string[] StatusNames => SuitData.status_applied;

        public SuitObject(ItemType type, int id, uint current_stack, bool inicialize_data = true) : base(type, id, current_stack, inicialize_data) { }

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