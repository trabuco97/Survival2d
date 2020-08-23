using UnityEngine;
using System.Collections;

namespace Survival2D.Entities.AI
{
    // Contains how the utility of each
    public enum ParameterMode { CustomCurve, NormalizedSigmodialCurve }

    // Sigmodial curve:
    // 0 - 50%-upperbound
    // 1 - 50%-lowerbound
    // 2 - is_reversed (-1 : yes, 1 : no)

    public class AIDataNode
    {
        public ParameterMode Mode { get; private set; }

        public AnimationCurve UtilityCurve { get; private set; }
        public float CurveMinValue { get; private set; }
        public float CurveMaxValue { get; private set; }

        public float[] AdditionalValues { get; private set; }

        public AIDataNode(ParameterMode mode, AnimationCurve curve)
        {
            Mode = mode;
            UtilityCurve = curve;
        }

        public AIDataNode(ParameterMode mode, float[] additional_values)
        {
            Mode = mode;
            AdditionalValues = additional_values;
        }

        public void SetMinMaxValues(float min_val, float max_val)
        {
            CurveMinValue = min_val;
            CurveMaxValue = max_val;
        }
    }
}