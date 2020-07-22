using System;
using UnityEngine;

namespace Survival2D.Systems.Statistics
{
    public enum StatModifierType
    {
        Flat,                   // the value is sum directly
        PercentAdd,             // the value is sum by a percentage of the value    100 as base value
        PercentMult,            // the value is multiplied by a percentage          100 as base value
    }

    [Serializable]
    public class StatModifier
    {
        public float value;
        public StatModifierType type;
        [Range(-1, 20)] public int order = -1;

        public void Init()
        {
            if (order == -1)
            {
                order = (int)type;
            }
        }
    }
}