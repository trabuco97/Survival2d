using UnityEngine;
using System.Collections;

public static class UnityHelper
{
    ///<summary> 
    /// First element is the top-left corner
    /// Second element is the bottom-right corner
    ///</summary>
    public static Vector3[] SpriteLocalToWorld(Transform transform, Sprite sprite_component)
    {
        Vector3 pos = transform.position;
        Vector3[] array = new Vector3[2];
        //top left
        array[0] = pos + sprite_component.bounds.min;
        // Bottom right
        array[1] = pos + sprite_component.bounds.max;
        return array;
    }
}
