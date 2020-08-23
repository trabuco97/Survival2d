using UnityEngine;

namespace Survival2D.Systems.Item.Inventory
{
    public class InventorySystemBehaviour : MonoBehaviour
    {
        // Hotbar reasons
        private const int INITIAL_PLAYER_SLOTS = 10;

        public InventorySystem Inventory { get; private set; } = null;

        private void Awake()
        {
            Inventory = new InventorySystem();
            Inventory.AddInventorySpace(INITIAL_PLAYER_SLOTS);
        }

    }
}