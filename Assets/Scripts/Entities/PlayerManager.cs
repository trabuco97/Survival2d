using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject player_object;

    private void Awake()
    {
        instance = this;
    }
}
