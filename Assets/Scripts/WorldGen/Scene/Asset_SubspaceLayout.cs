using UnityEngine;

using XNode;

namespace Survival2D.WorldGeneration
{
    [CreateAssetMenu(fileName = "New SceneSpace Layout", menuName = "Custom/WorldGen/Layout")]
    public class Asset_SubspaceLayout : NodeGraph
    {

        public SceneSpaceLayout GetLayout()
        {
            var node_collection = GetLayoutNodeCollection();
            var root_node = GetRootNode(node_collection);

            SubspaceLayoutNode root_output = GetLayoutNode(root_node);

            return new SceneSpaceLayout { root = root_output };

        }

        private SubspaceLayoutNode GetLayoutNode(Asset_SubspaceLayoutNode a_node)
        {
            if (a_node == null) return null;

            SubspaceLayoutNode output = new SubspaceLayoutNode();
            output.tag = a_node.tag;

            output.left = GetLayoutNode(a_node.LeftChild);
            output.right = GetLayoutNode(a_node.RightChild);
            output.up = GetLayoutNode(a_node.UpChild);
            output.down = GetLayoutNode(a_node.DownChild);

            return output;
        }

        private Asset_SubspaceLayoutNode[] GetLayoutNodeCollection()
        {
            var output = new Asset_SubspaceLayoutNode[nodes.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = nodes[i] as Asset_SubspaceLayoutNode;

            }

            return output;
        }

        private Asset_SubspaceLayoutNode GetRootNode(Asset_SubspaceLayoutNode[] array)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (array[i].is_root)
                {
                    return array[i];
                }
            }

            return null;
        }

    }
}