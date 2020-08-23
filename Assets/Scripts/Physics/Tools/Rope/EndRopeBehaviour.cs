using UnityEngine;
using System.Collections;

namespace Survival2D.Physics.Tools.Rope
{
    public class EndRopeBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform end_link = null;
        [SerializeField] private Rigidbody2D rgb2 = null;


        public Rigidbody2D Rgb2 { get { return rgb2; } }
        public Vector3 LocalEndJointPosition { get { return end_link.localPosition; } }
        public Vector3 EndJointPosition { get { return end_link.position; } }


    }
}