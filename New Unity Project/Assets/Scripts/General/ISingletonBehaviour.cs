using UnityEngine;
using System.Collections;

namespace Survival2D
{
    public abstract class ISingletonBehaviour<T> : MonoBehaviour
    {
        protected static T instance;
        public static T Instance { get { return instance; } }
    
        protected virtual void Awake()
        {
            if (instance != null) Destroy(this);
            else
            {
                SetInstace();
                DontDestroyOnLoad(this);
            }
        }

        protected abstract void SetInstace();
    }
}