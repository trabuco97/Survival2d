using UnityEngine;
using System.Collections;

namespace Survival2D
{
    public static class ProbabilityUtil
    {
        public static float[] GetChanceTableFromWeight<T>(T[] weight_array) where T : INodeWithWeight
        {
            float[] output = new float[weight_array.Length];
            float total_sum = 0;
            foreach (var wrapper in weight_array)
            {
                total_sum += wrapper.Weight;
            }

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = weight_array[i].Weight / total_sum;
            }

            return output;
        }



        // Pre: weight_array.Length > 0
        //      weight_array[i] > 0
        public static float[] GetChanceTableFromWeight(int[] weight_array)
        {
            float[] output = new float[weight_array.Length];
            float total_sum = 0;
            foreach (int weigth in weight_array)
            {
                total_sum += weigth;
            }

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = weight_array[i] / total_sum; 
            }

            return output;
        }


    }
}