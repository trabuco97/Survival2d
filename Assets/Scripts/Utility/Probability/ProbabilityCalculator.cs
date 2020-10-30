using UnityEngine;

namespace Survival2D
{
    public static class ProbabilityCalculator
    {
        // maybe used at first
        public static int GetElementFromChance<T>(T[] data_array, out T data_chosen, int init_seed, bool preserve_previous_state) where T : INodeWithFloatChance
        {
            var previous_state = Random.state;
            Random.InitState(init_seed);

            int output = GetElementFromChance(data_array, out T data_toChoose);
            data_chosen = data_toChoose;

            if (preserve_previous_state)
            {
                Random.state = previous_state;
            }

            return output;
        }


        /// Pre: sum of T.Chance in data_array is 100
        public static int GetElementFromChance<T>(T[] data_array, out T data_chosen) where T : INodeWithFloatChance
        {
            data_chosen = default(T);
            bool is_data_chosen = false;
            int i = 0;

            float chance = Random.Range(0f, 100f);
            float current_chance = 0;
            while (!is_data_chosen && i < data_array.Length)
            {
                current_chance += data_array[i].Chance;
                if (current_chance <= chance)
                {
                    is_data_chosen = true;
                    data_chosen = data_array[i];
                }
                else
                {
                    i++;
                }
            }

            return i;
        }


        public static int GetElementFromChance(float[] chance_array)
        {
            bool is_data_chosen = false;
            int i = 0;

            float chance = Random.Range(0f, 100f);
            float current_chance = 0;
            while (!is_data_chosen && i < chance_array.Length)
            {
                current_chance += chance_array[i];
                if (current_chance <= chance)
                {
                    is_data_chosen = true;
                }
                else
                {
                    i++;
                }
            }

            return i;
        }

        public static int GetEqualChanceElement(int array_length, int init_seed, bool preserve_previous_state)
        {
            var previous_state = Random.state;
            Random.InitState(init_seed);

            int output = GetEqualChanceElement(array_length);

            if (preserve_previous_state)
            {
                Random.state = previous_state;
            }

            return output;
        }

        public static int GetEqualChanceElement(int array_length)
        {
            bool is_data_chosen = false;
            int i = 0;

            float individual_chance = 1f / array_length;
            float chance = Random.Range(0f, 100f);
            float current_chance = 0;

            while (!is_data_chosen && i < array_length)
            {
                current_chance += individual_chance;
                if (current_chance <= chance)
                {
                    is_data_chosen = true;
                }
                else
                {
                    i++;
                }
            }

            return i;
        }

        public static int GetElementFromWeight<T>(T[] weight_array, out T weight_chosen) where T : INodeWithWeight
        {
            weight_chosen = default(T);

            // generate the chance table based on the table 
            int[] raw_array = new int[weight_array.Length];
            for (int i = 0; i < raw_array.Length; i++)
            {
                raw_array[i] = weight_array[i].Weight;
            }

            int index = GetElementFromWeight(raw_array);

            if (index >= 0 && index < weight_array.Length)
            {
                weight_chosen = weight_array[index];
            }

            return index;
        }

        public static int GetElementFromWeight(int[] weight_array)
        {
            float[] chance_array = ProbabilityUtil.GetChanceTableFromWeight(weight_array);
            int index = GetElementFromChance(chance_array);

            return index;
        }
    }

}