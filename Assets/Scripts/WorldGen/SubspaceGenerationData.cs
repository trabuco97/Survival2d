using UnityEngine;
using System.Collections;

namespace Survival2D.WorldGeneration
{
    public class SubspaceGenerationData : INodeWithFloatChance
    {
        public SubspaceData data;
        public float gen_chance;
        public GameObject subspace_prefab;

        public float Chance => gen_chance;
    }
}