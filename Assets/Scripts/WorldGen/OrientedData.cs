using UnityEngine;
using System.Collections;

namespace Survival2D.WorldGeneration
{
    public abstract class OrientedData<T>
    {
        public T left;
        public T right;
        public T up;
        public T down;
    }
}