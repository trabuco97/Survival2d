using UnityEngine;
using System.Collections;

namespace Survival2D.WorldGeneration
{
    public class SubspaceGeneratedBehaviour : MonoBehaviour
    {
        // All these anchors arent child of any object
        [SerializeField] private GameObject subscene_left_anchor = null;
        [SerializeField] private GameObject subscene_right_anchor = null;
        [SerializeField] private GameObject subscene_up_anchor = null;
        [SerializeField] private GameObject subscene_down_anchor = null;

        [SerializeField] private SubspaceTags room_tag = SubspaceTags.NONE;

        public Vector3 LeftAnchorWorldPosition { get { return subscene_left_anchor.transform.position; } }
        public Vector3 RightAnchorWorldPosition { get { return subscene_right_anchor.transform.position; } }
        public Vector3 UpAnchorWorldPosition { get { return subscene_up_anchor.transform.position; } }
        public Vector3 DownAnchorWorldPosition { get { return subscene_down_anchor.transform.position; } }

        public Vector3 LeftAnchorDistance { get { return subscene_left_anchor.transform.localPosition; } }
        public Vector3 RightAnchorDistance { get { return subscene_right_anchor.transform.localPosition; } }
        public Vector3 UpAnchorDistance { get { return subscene_up_anchor.transform.localPosition; } }
        public Vector3 DownAnchorDistance { get { return subscene_down_anchor.transform.localPosition; } }

        public Orientation GetOrientation()
        {
            Orientation output = Orientation.None;
            if (subscene_left_anchor != null)
            {
                output &= Orientation.Left;
            }

            if (subscene_right_anchor != null)
            {
                output &= Orientation.Right;
            }

            if (subscene_up_anchor != null)
            {
                output &= Orientation.Up;
            }

            if (subscene_down_anchor != null)
            {
                output &= Orientation.Down;
            }

            return output;
        }

        //public SubspaceData GetData()
        //{
        //    SubspaceData output = new SubspaceData();
        //    output.anchors = Orientation.None;

        //    if (subscene_left_anchor != null)
        //    {
        //        output.anchors &= Orientation.Left;
        //    }

        //    if (subscene_right_anchor != null)
        //    {
        //        output.anchors &= Orientation.Right;
        //    }

        //    if (subscene_up_anchor != null)
        //    {
        //        output.anchors &= Orientation.Up;
        //    }

        //    if (subscene_down_anchor != null)
        //    {
        //        output.anchors &= Orientation.Down;
        //    }

        //    output.tag = room_tag;
        //    return output;
        //}
    }
}