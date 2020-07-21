using UnityEngine;
using UnityEditor;
using Survival2D.Systems.Item;

[CustomEditor(typeof(ItemPickup))]
public class ItemPickupDisplay : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemPickup pickup = (ItemPickup)target;
        if (GUILayout.Button("Display Boundaries"))
        {
            pickup.is_boundaries_shown = true;
        }

        if (GUILayout.Button("Hide Boundaries"))
        {
            pickup.is_boundaries_shown = false;
        }
    }

}