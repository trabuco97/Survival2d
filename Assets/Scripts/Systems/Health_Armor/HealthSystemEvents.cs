using System;

using Survival2D.Systems.Item.Suit;

namespace Survival2D.Systems.HealthArmor
{
    public delegate void HealthMethods(HealthEventArgs args);
    public delegate void ArmorMethods(ArmorEventArgs args);

    public class HealthEventArgs : EventArgs
    {
        public float ActualHealth { get; private set; }
        public float TotalHealth { get; private set; }


        public HealthEventArgs(float actual_health, float total_health)
        {
            ActualHealth = actual_health;
            TotalHealth = total_health;
        }
    }

    public class ArmorEventArgs : EventArgs
    {
        public float ArmorValue { get; private set; }
        public float ActualArmorRating { get; private set; }
        public float TotalArmorRating { get; private set; }
        public SuitObject SuitEquipped { get; private set; }

        public ArmorEventArgs(SuitObject suit_equipped)
        {
            SuitEquipped = suit_equipped;
        }

        public ArmorEventArgs(float armor_value, float actual_armor_rating, float total_armor_rating)
        {
            ArmorValue = armor_value;
            ActualArmorRating = actual_armor_rating;
            TotalArmorRating = total_armor_rating;
        }

    }

}
