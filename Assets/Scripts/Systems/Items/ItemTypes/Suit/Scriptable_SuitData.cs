using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival2D.Systems.Item.Suit
{
    [CreateAssetMenu(fileName = "New SuitData file", menuName = "Custom/Item/Suit")]
    public class Scriptable_SuitData : Scriptable_IItemData
    {
        [Header("Armor Data")]
        [Range(0, 100)] public int base_damage_reduction;
        public int base_armor_rating;
        public int base_armor_rating_loss;
        public int base_radiation_protection;

        public string[] status_applied;
    }
}