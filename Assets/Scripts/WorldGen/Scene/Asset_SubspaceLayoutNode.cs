using System;
using UnityEngine;

using XNode;


namespace Survival2D.WorldGeneration
{
    [CreateNodeMenu("SubspaceLayoutNode")]
    public class Asset_SubspaceLayoutNode : Node
    {
        public bool is_root = false;
        public SubspaceTags tag;

        [Input] public Asset_SubspaceLayoutNode LeftChild;
        [Input] public Asset_SubspaceLayoutNode RightChild;
        [Input] public Asset_SubspaceLayoutNode UpChild;
        [Input] public Asset_SubspaceLayoutNode DownChild;

        [Output] public Asset_SubspaceLayoutNode ChildOf;

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);

            LeftChild = GetInputValue<Asset_SubspaceLayoutNode>("LeftChild");
            RightChild = GetInputValue<Asset_SubspaceLayoutNode>("RightChild");
            UpChild = GetInputValue<Asset_SubspaceLayoutNode>("UpChild");
            DownChild = GetInputValue<Asset_SubspaceLayoutNode>("DownChild");
        }

        public override object GetValue(NodePort port)
        {
            return this;
        }
    }
}