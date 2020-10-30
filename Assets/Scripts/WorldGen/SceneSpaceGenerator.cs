using System.Collections.Generic;
using UnityEngine;



namespace Survival2D.WorldGeneration
{

    [System.Flags]
    public enum Orientation
    {
        None = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Up = 1 << 2,
        Down = 1 << 3,
    }


    public static class SceneSpaceGenerator
    {
        // each SubspaceGenerationData has an uniquely id
        // all colllections of SubspaceGenerationData have the sum of all .gen_chance equal to 100
        private static SubspaceGenerationData[] random_room_toGenerate = null;
        // each value has the same SubspaceGenerationData.SubspaceData.tag and each element has a unique common tags
        // each element has List<SubspaceGenerationData>.Count > 0
        private static Dictionary<SubspaceTags, SubspaceGenerationData[]> disposable_taggedRooms_toGenerate;

        public static void InitializeGenerator(SubspaceGenerationData[] random_room_toGenerate_array, List<SubspaceGenerationData>[] tagged_rooms_togenerate)
        {
            random_room_toGenerate = random_room_toGenerate_array;

            disposable_taggedRooms_toGenerate = new Dictionary<SubspaceTags, SubspaceGenerationData[]>();
            // populate the dictionary
            foreach (var list in tagged_rooms_togenerate)
            {
                var tag = list[0].data.tag;
                disposable_taggedRooms_toGenerate.Add(tag, list.ToArray());
            }
        }

        /// <summary>
        /// 1 - chose which layout to use as template
        /// 
        /// 
        /// </summary>
        public static SubspaceNode GenerateSpace(SceneSpaceLayout layout, int seed)
        {
            // set the current seed to generate
            Random.State previous_state = Random.state;
            Random.InitState(seed);


            // get the tree by root of the assigned id room
            SubspaceNode output = GetSubspaceNode(layout.root);

            Random.state = previous_state;

            return output;
        }

        private static SubspaceNode GetSubspaceNode(SubspaceLayoutNode layout_node)
        {
            if (layout_node == null) return null;

            SubspaceNode output = new SubspaceNode();

            // generate the id based on if its tagged or not
            var orientation_toMatch = GetOrientation(layout_node);

            if (layout_node.tag == SubspaceTags.NONE)
            {
                // Get collection of rooms that matches with the layout node
                var matching_random_rooms = GetMatchingDataArray(random_room_toGenerate, orientation_toMatch);

                ProbabilityCalculator.GetElementFromChance(matching_random_rooms, out var gen_data);
                if (gen_data != null)
                {
                    output.subspace_prefab = gen_data.subspace_prefab;
                }
            }
            else
            {
                SubspaceTags tag = layout_node.tag;
                if (disposable_taggedRooms_toGenerate.ContainsKey(tag))
                {

                    var tagged_list = disposable_taggedRooms_toGenerate[tag];
                    // Get collection of rooms that matches with the layout node
                    var matching_tagged_rooms = GetMatchingDataArray(tagged_list, orientation_toMatch);

                    ProbabilityCalculator.GetElementFromChance(matching_tagged_rooms, out var gen_data);
                    if (gen_data != null)
                    {
                        output.subspace_prefab = gen_data.subspace_prefab;
                    }
                }
                else
                {
                    Debug.LogError("ERROR: Trying to generate a tag from layout that isnt specified by the scene");
                }
            }

            // Get childs of layoutnode
            output.left = GetSubspaceNode(layout_node.left);
            output.right = GetSubspaceNode(layout_node.right);
            output.up = GetSubspaceNode(layout_node.up);
            output.down = GetSubspaceNode(layout_node.down);

            return output;
        }

        private static Orientation GetOrientation<T>(OrientedData<T> node)
        {
            Orientation output = Orientation.None;
            if (node.left != null)
            {
                output &= Orientation.Left;
            }
            if (node.right != null)
            {
                output &= Orientation.Right;
            }
            if (node.up != null)
            {
                output &= Orientation.Up;
            }
            if (node.down != null)
            {
                output &= Orientation.Down;
            }

            return output;
        }

        private static SubspaceGenerationData[] GetMatchingDataArray(SubspaceGenerationData[] random_room_toGenerate, Orientation orientations_toMatch)
        {
            var output_list = new List<SubspaceGenerationData>();
            foreach (var gen_data in random_room_toGenerate)
            {
                if (gen_data.data.anchors == orientations_toMatch)
                {
                    output_list.Add(gen_data);
                }
            }

            return output_list.ToArray();
        }

    }
}