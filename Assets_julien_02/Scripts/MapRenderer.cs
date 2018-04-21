using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    private Camera cam;
    private Player player;

    public LayerMask obstacleMask;
    public float minimumAngle = 10f;

    private void Start()
    {
        cam = Camera.main;
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        ViewObstacles();

        Vector3 camPos = cam.transform.position;
        Vector3 playerPos = player.transform.position;
        Vector3 dirToPlayer = new Vector3(playerPos.x - camPos.x, camPos.y, playerPos.z - camPos.z);
        for (int i = 0; i < Map.invisibleMap.Count; i++)
        {
            Vector3 dirToObstacle = new Vector3(Map.invisibleMap[i].Position.x - camPos.x, camPos.y, Map.invisibleMap[i].Position.z - camPos.z);
            float angle = Vector3.Angle(dirToObstacle, dirToPlayer);

            if (angle > minimumAngle)
                Map.RemoveInvisible(Map.invisibleMap[i]);
            else
                Map.invisibleMap[i].SetVisibility(false);
        }
    }

    //----------------------------------------------------------------------------------------------------
    // Render only the obstacles that are in the scene and do not hide the path of the player
    //----------------------------------------------------------------------------------------------------
    private void ViewObstacles()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 dir = player.transform.position - cam.transform.position;
        float dst = Vector3.Distance(player.transform.position, cam.transform.position);
        Ray ray = new Ray(cam.transform.position, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, dst, obstacleMask))
        {
            Map.AddInvisible(hit.collider.transform.position);
        }
    }
}