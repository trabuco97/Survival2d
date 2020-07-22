using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Survival2D.Input;
using Survival2D.UI.Item.Inventory;
using Survival2D.UI.Item.Equipment;
using Survival2D.Systems.Item;
using Survival2D.Systems.HealthArmor;

namespace Survival2D
{
    public class GameManager : MonoBehaviour
    {
        public Canvas ui_inventory_canvas;

        public UI_Inventory inventory_ui;
        public UI_Equipment equipment_ui;
        public PlayerGameplayInput inputActions;

        public HealthArmorSystemBehaviour healthArmorSystem;
        public ItemPickGeneratorBehaviour generator;

        private void Awake()
        {
            inputActions = new PlayerGameplayInput(); 
        }

        private void Start()
        {
            SetInventoryCanvasState(false);
        }

        private void SetInventoryCanvasState(bool state)
        {
            ui_inventory_canvas.gameObject.SetActive(state);
        }


        private void Update()
        {
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }
    }
}