using UnityEngine;

namespace Survival2D.Entities.AI
{
    public static class CurveResponseCalculator
    {

        public static float GetValueFromEquation(AIDataNode node, float input)
        {
            if (node.Mode == ParameterMode.CustomCurve)
            {
                // Displace all values assuming min value = 0
                float new_input = input - node.CurveMinValue;
                float new_max_value = node.CurveMaxValue - node.CurveMinValue;

                // TODO: max value cant be 0, maybe change smoething
                float normalized_input = new_input / new_max_value;
                return node.UtilityCurve.Evaluate(normalized_input);
            }
            else if (node.Mode == ParameterMode.NormalizedSigmodialCurve)
            {
                float uppB = node.AdditionalValues[0];
                float lowB = node.AdditionalValues[1];
                float sign = node.AdditionalValues[0];  // -1 or 1

                float a = (uppB + lowB) / 2;
                float b = 2 / Mathf.Abs(lowB - uppB);
                float c = 0;
                float d = 1 - c;

                return c + (d / (1 + Mathf.Pow(Mathf.Epsilon, sign * b * (input - a))));
            }

            return 0f;
        }
    }
}