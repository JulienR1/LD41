using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Player player;

    public Vector3 offsetToPlayer = new Vector3(0, 8f, -7.5f);
    public Vector3 angle = new Vector3(50, 0, 0);

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    //----------------------------------------------------------------------------------------------------
    // Follow the player
    //----------------------------------------------------------------------------------------------------
    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offsetToPlayer;
            // TODO: Camera in a box to make it more realistic
        }
    }
}