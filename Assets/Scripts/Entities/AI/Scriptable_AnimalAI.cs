using System;
using UnityEngine;

namespace Survival2D.Entities.AI
{
    [CreateAssetMenu(fileName = "New Animal AI", menuName = "Custom/AI/Animal")]
    public class Scriptable_AnimalAI : ScriptableObject
    {
        #region WRAPPERS
        [Serializable]
        public class IActionWrapper
        {
            public ActionType type;
            public float weight;
            public IConsiderationWrapper[] consideration_container;
        }

        [Serializable]
        public class IConsiderationWrapper
        {
            public ConsiderationType type;

            public ParameterMode mode;
            public AnimationCurve utility_curve;
            public float[] aditional_values;
        }

        #endregion

        public IActionWrapper[] actions;

    }
}