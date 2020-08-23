using System.Collections.Generic;
using UnityEngine;

namespace Survival2D.Physics.Tools.Rope
{
    public class Deprecated_RopeBehaviour : MonoBehaviour
    {
        [Tooltip("mesured in instance of the link prefab")]
        [SerializeField] private uint length = 2;
        [Range(1, 20)] [SerializeField] private uint detail_factor = 1;
        [SerializeField] private float link_offset = 0.0000001f;

        [Header("References")]
        [SerializeField] private GameObject link_prefab = null;

        [SerializeField] private bool has_weight_prefab = false;
        [SerializeField] private GameObject weight_end = null;
        [SerializeField] private GameObject weight_end_prefab = null;
        [SerializeField] private Rigidbody2D parent_rgb2 = null;

        [Header("values for independent generation")]
        [SerializeField] private Vector2 awake_rope_direction = Vector2.down;

        private Vector3[] link_bounds;
        private Stack<RopeLinkBehaviour> rope_stack = new Stack<RopeLinkBehaviour>();

        private float link_length = 0;

        public GameObject WeightObject { get; private set; } = null;


        private void Awake()
        {
#if UNITY_EDITOR

            if (link_prefab == null)
            {
                Debug.LogWarning($"{nameof(link_prefab)} is not assigned to {nameof(Deprecated_RopeBehaviour)} of {gameObject.GetFullName()}");
            }

            if (weight_end == null && !has_weight_prefab)
            {
                Debug.LogWarning($"{nameof(weight_end)} is not assigned to {nameof(Deprecated_RopeBehaviour)} of {gameObject.GetFullName()}");
            }


            if (weight_end_prefab == null && has_weight_prefab)
            {
                Debug.LogWarning($"{nameof(weight_end_prefab)} is not assigned to {nameof(Deprecated_RopeBehaviour)} of {gameObject.GetFullName()}");
            }

            if (parent_rgb2 == null)
            {
                Debug.LogWarning($"{nameof(parent_rgb2)} is not assigned to {nameof(Deprecated_RopeBehaviour)} of {gameObject.GetFullName()}");
            }
#endif

            if (!has_weight_prefab) weight_end.SetActive(false);

            link_bounds = UnityHelper.SpriteLocalToWorld(link_prefab.transform, link_prefab.GetComponentInChildren<SpriteRenderer>().sprite);
            link_length = (link_bounds[0].y - link_bounds[1].y);


            link_length = 1 / detail_factor;
        }

        public void GenerateLink(Transform origin_transform, Vector2 direction, ref float current_displacement)
        {
            direction = direction.normalized;

            var link_length = (link_bounds[0].y - link_bounds[1].y);
            var link_instance = Instantiate(link_prefab, origin_transform, false);
            var link_behaviour = link_instance.GetComponent<RopeLinkBehaviour>();

            if (rope_stack.Count == 0)
            {
                // Add to the rope another hinge and attaches it to the parent_rgb2
                link_behaviour.ParentHinge.connectedBody = parent_rgb2;
                link_behaviour.ParentHinge.anchor = link_behaviour.ParentHinge.anchor = new Vector2(0, 0.5f + link_offset);
            }
            else
            {
                // Connect the last link to the current link
                var last_link = rope_stack.Peek();

                last_link.NextLinkHinge.connectedBody = link_behaviour.Rgb2;
                last_link.NextLinkHinge.anchor = direction * (-0.5f - link_offset / 2);
                last_link.NextLinkHinge.connectedAnchor = -last_link.NextLinkHinge.anchor;
                last_link.Rgb2.mass /= detail_factor;

                link_behaviour.ParentHinge.enabled = false;
            }

            var scale_factor = link_instance.transform.localScale;
            scale_factor.y /= detail_factor;
            link_instance.transform.localScale = scale_factor;
            link_instance.transform.position += (Vector3)(direction * current_displacement);

            current_displacement += link_length + link_offset;
            rope_stack.Push(link_behaviour);
        }

        public void LinkWeightToRope(Transform origin_transform, Vector2 direction, ref float current_displacement)
        {
            // Inicialize the weightobject
            if (has_weight_prefab) WeightObject = Instantiate(weight_end_prefab, origin_transform, false);
            else
            {
                WeightObject = weight_end;
                WeightObject.SetActive(true);
            }

            WeightObject.transform.position += (Vector3)(direction * current_displacement);

            // link the weight to the rope
            var last_link = rope_stack.Peek();

            last_link.NextLinkHinge.connectedBody = WeightObject.GetComponent<Rigidbody2D>();
            last_link.NextLinkHinge.anchor = direction * (-0.5f - link_offset / 2);
            last_link.NextLinkHinge.connectedAnchor = -last_link.NextLinkHinge.anchor;
            last_link.Rgb2.mass /= detail_factor;

            if (WeightObject.TryGetComponent(out DistanceJoint2D distance_joint))
            {
                distance_joint.distance = current_displacement;
            }

        }

        public void GenerateRope(Transform transform, Vector2 direction)
        {
            float current_displacement = 0;

            for (int i = 0; i < length * detail_factor; i++)
            {
                GenerateLink(transform, direction, ref current_displacement);
            }

            LinkWeightToRope(transform, direction, ref current_displacement);
        }

        public void GenerateRope(Vector2 origin, Vector2 end)
        {

        }
         
        [ContextMenu("generate rope")]
        public void GenerateRope()
        {
            GenerateRope(parent_rgb2.transform, awake_rope_direction);
        }
    }
}