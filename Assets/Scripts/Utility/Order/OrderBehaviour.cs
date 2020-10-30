using System;
using System.Collections.Generic;
using UnityEngine;

namespace Survival2D
{
    // To each gameobject with components that need an ordered initialization for initialization
    public class OrderBehaviour : MonoBehaviour
    {
        #region WRAPPER
        [Serializable]
        private class OrderLayer
        {
            public int order;
            public IOrderedBehaviour[] behaviour_container;
        }
        #endregion


        private List<OrderLayer> layer_container = new List<OrderLayer>();

        public event EventHandler OnObjectInitialized;

        private void Awake()
        {
            // Generate all the layers
            var ordered_list = new List<IOrderedBehaviour>(GetComponentsInChildren<IOrderedBehaviour>());

            while (ordered_list.Count != 0)
            {
                var ordered = ordered_list[0];
                var layer = new OrderLayer();
                layer.order = ordered.Order;

                var layer_list = new List<IOrderedBehaviour>();
                layer_list.Add(ordered);
                ordered_list.RemoveAt(0);

                int i = 0;
                while (i < ordered_list.Count)
                {
                    if (ordered_list[i].Order == layer.order)
                    {
                        layer_list.Add(ordered_list[i]);
                        ordered_list.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

                layer.behaviour_container = layer_list.ToArray();
                layer_container.Add(layer);
            }

            // Sort layers
            // first_layer == 1
            layer_container.Sort((a, b) =>
            {
                if (a.order < b.order)
                    return -1;
                else if (a.order > b.order)
                    return 1;
                else
                    return 0;
            });
        }

        public void InitializeObject()
        {
            foreach (var layer in layer_container)
            {
                foreach (var behaviour in layer.behaviour_container)
                {
                    behaviour.Initialize();
                }
            }


            OnObjectInitialized?.Invoke(this, EventArgs.Empty);
        }

    }
}