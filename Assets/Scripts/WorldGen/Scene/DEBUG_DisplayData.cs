using UnityEngine;
using System.Collections;

namespace Survival2D.WorldGeneration
{
    public class DEBUG_DisplayData : MonoBehaviour
    {
        public Asset_SubspaceLayout layout;


        [ContextMenu("Displaydata")]
        public void D1()
        {
            r_DisplayNode(layout.GetLayout().root);
        }

        public void r_DisplayNode(SubspaceLayoutNode node)
        {
            if (node == null) return;

            Debug.Log(node.tag);

            r_DisplayNode(node.left);
            r_DisplayNode(node.right);
            r_DisplayNode(node.up);
            r_DisplayNode(node.down);
        }

    }
}