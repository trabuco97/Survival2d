using UnityEngine;
using System.Collections;

namespace Survival2D.Systems.Item.Harpoon
{
    [CreateAssetMenu(fileName = "NEw Harpoon Data", menuName = "Custom/Item/Harpoon")]
    public class Scriptable_HarpoonData : Scriptable_IItemData
    {
        [Header("Harpoon data")]
        public float base_harpoon_impulse;
        public float base_harpoon_retract;
        public float base_harpoon_damage;
    }
}