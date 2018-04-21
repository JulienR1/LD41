using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //----------------------------------------------------------------------------------------------------
    // Create the map
    //----------------------------------------------------------------------------------------------------
    private void GenerateNewMap()
    {
        Map.SetSize(10, 10);

        for (int x = 0; x < Map.Width; x++)
        {
            for (int y = 0; y < Map.Height; y++)
            {
                Debug.Log(Mathf.PerlinNoise(x, y));
            }
        }
    }
}