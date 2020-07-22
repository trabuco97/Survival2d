using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Survival2D.Systems.Item.Equipment
{
    public class EquipmentSystemBehaviour : MonoBehaviour
    {
        public EquipmentSystem Equipment { get; private set; } = null;


        private void Awake()
        {
            Equipment = new EquipmentSystem();
        }
    }
}