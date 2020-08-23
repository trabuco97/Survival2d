using System.Collections.Generic;
using UnityEngine;

namespace Survival2D.Physics.Tools.Rope
{
    // TODO: Optimize the rendering of the line
    public class RopeBehaviour : MonoBehaviour
    {
        private class RopeRendererWrapper
        {
            public Vector3 local_point;
            public Transform local_transform;
        }

        [SerializeField] private uint hinge_number = 1;
        [Range(1, 20)] [SerializeField] private uint line_precision = 1;
        [SerializeField] private GameObject hinge_object_prefab = null;

        [SerializeField] private Rigidbody2D origin_rgb2 = null;            // origin is the parent
        [SerializeField] private Transform origin_transform = null;            // origin is the parent
        [SerializeField] private EndRopeBehaviour end_rope = null;          // end 

        [SerializeField] private LineRenderer rope_renderer = null;         // Line renderer uses world position to render the rope

        private Queue<RopeRendererWrapper> rope_point_info_queue = null;
        private List<Vector3> rope_points = null;


        private Stack<RopeLinkBehaviour> rope_stack = new Stack<RopeLinkBehaviour>();


        public bool IsInicialized { get; private set; }


        private void Update()
        {
            if (IsInicialized)
            {
                RecalculateRopePointsInfo();
                GenerateDisplay();
            }
        }

        [ContextMenu("Generate Rope")]
        public void GenerateRope()
        {
            if (IsInicialized) return;
            rope_point_info_queue = new Queue<RopeRendererWrapper>();

            // Calculations are performed assuming:
            // - origin position (0, 0)
            // - end position is in local space of the origin

            var vector_rope = end_rope.transform.localPosition;
            var direction = vector_rope.normalized;
            var distance = vector_rope.magnitude;
            var hinge_length = distance / hinge_number;

            RopeLinkBehaviour last_hinge;
            Vector3 world_joint_point;

            for (int i = 0; i < hinge_number; i++)
            {
                var new_local_position = direction * ((i * hinge_length) + (hinge_length / 2));

                var hinge_instance = Instantiate(hinge_object_prefab, this.transform, false);
                hinge_instance.transform.localPosition = new_local_position;


                var hinge = hinge_instance.GetComponent<RopeLinkBehaviour>();
                if (i == 0)
                {
                    hinge.ParentHinge.connectedBody = origin_rgb2;
                    hinge.ParentHinge.anchor = hinge.transform.InverseTransformPoint(this.transform.position);
                    hinge.ParentHinge.connectedAnchor = origin_transform.localPosition;

                    rope_point_info_queue.Enqueue(new RopeRendererWrapper { local_point = hinge.ParentHinge.anchor, local_transform = hinge.transform });
                }
                else
                {

                    last_hinge = rope_stack.Peek();
                    last_hinge.NextLinkHinge.connectedBody = hinge.Rgb2;

                    world_joint_point = transform.TransformPoint(direction * (i * hinge_length));
                    last_hinge.NextLinkHinge.anchor = last_hinge.transform.InverseTransformPoint(world_joint_point);
                    last_hinge.NextLinkHinge.connectedAnchor = hinge.transform.InverseTransformPoint(world_joint_point);

                    hinge.ParentHinge.enabled = false;
                    rope_point_info_queue.Enqueue(new RopeRendererWrapper { local_point = last_hinge.ParentHinge.anchor, local_transform = last_hinge.transform });
                }

                rope_stack.Push(hinge);
            }

            // Link the hinge to the end rgb2
            last_hinge = rope_stack.Peek();
            last_hinge.NextLinkHinge.connectedBody = end_rope.Rgb2;

            //world_joint_point = transform.TransformPoint(direction * (hinge_number * hinge_length));

            world_joint_point = end_rope.EndJointPosition;
            last_hinge.NextLinkHinge.anchor = last_hinge.transform.InverseTransformPoint(world_joint_point);
            last_hinge.NextLinkHinge.connectedAnchor = end_rope.LocalEndJointPosition;

            rope_point_info_queue.Enqueue(new RopeRendererWrapper { local_point = last_hinge.ParentHinge.anchor, local_transform = last_hinge.transform });

            IsInicialized = true;
        }

        [ContextMenu("Destroy Rope")]
        public void DestroyRope()
        {
            rope_renderer.positionCount = 0;
            rope_point_info_queue = null;

            while (rope_stack.Count != 0)
            {
                var rope_link = rope_stack.Pop();
                Destroy(rope_link.gameObject);
            }

            IsInicialized = false;
        }

        private void RecalculateRopePointsInfo()
        {
            rope_points = new List<Vector3>();

            var info_aux = new Queue<RopeRendererWrapper>(rope_point_info_queue);

            RopeRendererWrapper start_rope_point = info_aux.Dequeue();
            RopeRendererWrapper[] aux = new RopeRendererWrapper[4];
            Vector3[] points = new Vector3[4];
            bool is_beginning = true;
            while (info_aux.Count != 0)
            {

                if (info_aux.Count > 2)
                {
                    aux[0] = start_rope_point;
                    aux[1] = info_aux.Dequeue();
                    aux[2] = info_aux.Dequeue();
                    aux[3] = info_aux.Dequeue();

                    start_rope_point = aux[3];

                    points[0] = GetRopePoint(aux[0]);
                    points[1] = GetRopePoint(aux[1]);
                    points[2] = GetRopePoint(aux[2]);
                    points[3] = GetRopePoint(aux[3]);


                    GenerateRopePoints(points[0], points[1], points[2], points[3], is_beginning);
                }
                else if (info_aux.Count > 1)
                {
                    aux[0] = start_rope_point;
                    aux[1] = info_aux.Dequeue();
                    aux[2] = info_aux.Dequeue();

                    start_rope_point = aux[2];

                    points[0] = GetRopePoint(aux[0]);
                    points[1] = GetRopePoint(aux[1]);
                    points[2] = GetRopePoint(aux[2]);

                    GenerateRopePoints(points[0], points[1], points[2], is_beginning);
                }
                else if (info_aux.Count > 0)
                {
                    if (is_beginning)
                    {
                        rope_points.Add(GetRopePoint(start_rope_point));
                    }

                    rope_points.Add(GetRopePoint(info_aux.Dequeue()));
                }


                is_beginning = false;
            }
        }

        // Get point in world coordinates
        private Vector3 GetRopePoint(RopeRendererWrapper wrapper)
        {
            return wrapper.local_transform.TransformPoint(wrapper.local_point);
        }

        private void GenerateDisplay()
        {
            rope_renderer.positionCount = rope_points.Count;
            rope_renderer.SetPositions(rope_points.ToArray());
        }


        private void GenerateRopePoints(Vector3 origin, Vector3 middle, Vector3 end, bool is_beginning = false)
        {
            for (int i = 0; i <= line_precision; i++)
            {
                if (i == 0 && !is_beginning) continue;

                // Using the quadratic bezier formula to calculate the curve
                float t = (float)i / (float)line_precision;
                var point = middle + Mathf.Pow(1 - t, 2) * (origin - middle) + Mathf.Pow(t, 2) * (end - middle);
                rope_points.Add(point);
            }
        }

        private void GenerateRopePoints(Vector3 origin, Vector3 middle1, Vector3 middle2, Vector3 end, bool is_beginning = false)
        {
            for (int i = 0; i <= line_precision; i++)
            {
                if (i == 0 && !is_beginning) continue; 

                // Using the cubic bezier formula to calculate the curve
                float t = (float)i / (float)line_precision;
                var point = Mathf.Pow(1 - t, 3) * origin + 3 * Mathf.Pow(1 - t, 2) * t * middle1 + 3 * (1 - t) * Mathf.Pow(t, 2) * middle2 + Mathf.Pow(t, 3) * end;
                rope_points.Add(point);
            }
        }
    }
}