﻿using UnityEngine;

namespace Assets.IUnified.Sample
{
    public class MyInterfaceImplementation : MonoBehaviour, IMyInterface
    {
        public string StringProperty { get  { return $"{nameof(MyInterfaceImplementation)}.{nameof(StringProperty)}"; } }

        public void Method()
        {
            Debug.Log($"{nameof(MyInterfaceImplementation)}.{nameof(Method)}()");
        }
    }
}