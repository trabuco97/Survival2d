using UnityEngine;

namespace Survival2D.Systems.Item.Inventory
{
    public class InventorySystemBehaviour : MonoBehaviour
    {
        public InventorySystem Inventory { get; private set; } = null;

        private void Awake()
        {
            Inventory = new InventorySystem();
        }

    }
}