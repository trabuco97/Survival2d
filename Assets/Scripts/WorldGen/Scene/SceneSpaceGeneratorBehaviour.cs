﻿using UnityEngine;

namespace Survival2D.WorldGeneration
{
    public class SceneSpaceGeneratorBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform root_transform = null;

        [SerializeField] private Asset_SubspaceLayout[] layouts_toUse = null;
        [SerializeField] private Asset_SubspaceChanceTable chance_table = null;

        private void Awake()
        {
            if (root_transform == null)
            {
                root_transform = this.transform;
            }
        }

        public void GenerateNewSpace(int seed)
        {
            // first, gets the tree to generate the room
            // from a layout selected randomly
            int layout_i = ProbabilityCalculator.GetEqualChanceElement(layouts_toUse.Length);
            SceneSpaceLayout layout = layouts_toUse[layout_i].GetLayout();

            // then, get the tree of subspaces generated by the generator
            var random_chance_table = chance_table.GetGenericChanceTable();
            var tagged_chance_tables = chance_table.GetTaggedChanceTables();

            SceneSpaceGenerator.InitializeGenerator(random_chance_table, tagged_chance_tables);
            SubspaceNode root = SceneSpaceGenerator.GenerateSpace(layout, seed);

            // finally, instanciate the subscenes in the scenespace
            var current = Instantiate(root.subspace_prefab, root_transform.position, Quaternion.identity).GetComponent<SubspaceGeneratedBehaviour>();
            GenerateSubspace(current, root.left, Orientation.Left);
            GenerateSubspace(current, root.right, Orientation.Right);
            GenerateSubspace(current, root.up, Orientation.Up);
            GenerateSubspace(current, root.down, Orientation.Down);
        }

        private void GenerateSubspace(SubspaceGeneratedBehaviour parent, SubspaceNode node, Orientation orientation_toGen)
        {
            if (node == null) return;

            var current = node.subspace_prefab;
            var current_behaviour = AlignSubscene(parent, current, orientation_toGen);

            GenerateSubspace(current_behaviour, node.left, Orientation.Left);
            GenerateSubspace(current_behaviour, node.right, Orientation.Right);
            GenerateSubspace(current_behaviour, node.up, Orientation.Up);
            GenerateSubspace(current_behaviour, node.down, Orientation.Down);
        }


        private SubspaceGeneratedBehaviour AlignSubscene(SubspaceGeneratedBehaviour root, GameObject next_prefab, Orientation orientation_toGen) 
        {
            SubspaceGeneratedBehaviour output = Instantiate(next_prefab).GetComponent<SubspaceGeneratedBehaviour>();
            Vector3 spawn_pos = Vector3.zero;

            if (orientation_toGen == Orientation.Left)
            {
                spawn_pos = root.LeftAnchorWorldPosition - output.RightAnchorDistance;
            }
            else if (orientation_toGen == Orientation.Right)
            {
                spawn_pos = root.RightAnchorWorldPosition - output.LeftAnchorDistance;
            }
            else if (orientation_toGen == Orientation.Up)
            {
                spawn_pos = root.UpAnchorWorldPosition - output.DownAnchorDistance;
            }
            else if (orientation_toGen == Orientation.Down)
            {
                spawn_pos = root.DownAnchorWorldPosition - output.UpAnchorDistance;

            }

            output.transform.position = spawn_pos;
            return output;

        }

#if UNITY_EDITOR
        [ContextMenu("a")]
        public void A()
        {
            GenerateNewSpace(0);
        }
#endif

    }
}