using System;
using UnityEngine;
using UnityEngine.Events;

using Survival2D.Systems.Item.Suit;

namespace Survival2D.Systems.HealthArmor
{


    /// <summary>
    /// 0 - actual health
    /// 1 - total health 
    /// </summary>
    [Serializable]
    public class HealthModificationEvent : UnityEvent<float, float> { }

    /// <summary>
    /// 0 - actual rating
    /// 1- total rating
    /// </summary>
    [Serializable]
    public class ArmorRatingModificiationEvent : UnityEvent<float, float> { }


    /// <summary>
    /// 0 - total health
    /// </summary>
    [Serializable]
    public class ZeroHealthEvent : UnityEvent<float> { }


    public class ArmorAdquiredEventInfo
    {
        public float armor_value;
        public float armor_rating_temp_value;
        public float armot_rating_total_value;
    }

    [Serializable]
    public class ArmorAdquiredEvent : UnityEvent<ArmorAdquiredEventInfo> { }

    [Serializable]
    public class ZeroArmorRatingEvent : UnityEvent<SuitObject> { }
}
