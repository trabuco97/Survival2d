using System.Collections.Generic;
using UnityEngine;

namespace Survival2D.WorldGeneration
{
    [CreateAssetMenu(fileName = "Chance_Table", menuName = "Custom/WorldGen/SubspaceChanceTable")]
    public class Asset_SubspaceChanceTable : ScriptableObject
    {
        #region WRAPPERS
        [System.Serializable]
        private class TaggedWrapper
        {
            public SubspaceTags tag = SubspaceTags.NONE;
            public WeightWrapper[] weights = null;
        }

        [System.Serializable]
        private class WeightWrapper : INodeWithWeight
        {
            public GameObject subspace_prefab = null;
            [Range(0, 20)]
            public int weight = 0;

            public int Weight => weight;
        }
        #endregion

        [SerializeField] private WeightWrapper[] generic_room_chances = null;
        [SerializeField] private TaggedWrapper[] tagged_room_chances = null;

        public SubspaceGenerationData[] GetGenericChanceTable()
        {
            return GetChanceTable(generic_room_chances);
        }

        public List<SubspaceGenerationData>[] GetTaggedChanceTables()
        {
            List<SubspaceGenerationData>[] output = new List<SubspaceGenerationData>[tagged_room_chances.Length];
            for (int i = 0; i < output.Length; i++)
            {
                var array = GetChanceTable(tagged_room_chances[i].weights, tagged_room_chances[i].tag);
                output[i] = new List<SubspaceGenerationData>(array);
            }

            return output;
        }

        private static SubspaceGenerationData[] GetChanceTable(WeightWrapper[] weight_array, SubspaceTags subspace_tag = SubspaceTags.NONE)
        {
            SubspaceGenerationData[] output = new SubspaceGenerationData[weight_array.Length];
            float[] chance_table = ProbabilityUtil.GetChanceTableFromWeight(weight_array);


            for (int i = 0; i < weight_array.Length; i++)
            {
                var weight_data = weight_array[i];

                if (weight_data.subspace_prefab.TryGetComponent(out SubspaceGeneratedBehaviour generated))
                {
                    output[i] = new SubspaceGenerationData
                    {
                        data = new SubspaceData { anchors = generated.GetOrientation(), tag = subspace_tag },
                        gen_chance = chance_table[i],
                        subspace_prefab = weight_data.subspace_prefab
                    };
                }
#if UNITY_EDITOR
                else 
                {
                    Debug.LogError("ERROR, prefab of subspace doesnt have SubspaceGeneratedBehaviour component");
                }
#endif


            }

            return output;
        }

    }
}