using UnityEngine;
using System.Collections;

namespace Survival2D.Physics.Tools.Rope
{
    public class RopeLinkBehaviour : MonoBehaviour
    {
        [SerializeField] private HingeJoint2D next_link_hinge = null;
        [SerializeField] private HingeJoint2D parent_hinge = null;
        [SerializeField] private Rigidbody2D rgb2 = null;


        public HingeJoint2D NextLinkHinge { get { return next_link_hinge; } }
        public HingeJoint2D ParentHinge { get { return parent_hinge; } }
        public Rigidbody2D Rgb2 { get { return rgb2; } }
    }
}