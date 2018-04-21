using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform obstacleT;
    public Transform borderT;
    public Transform doorT;

    [Header("Settings")]
    public Vector2Int mapSize;
    public bool renderTexture, buildMap;
    private float xOffset, yOffset;
    public float scale = 1;
    [Range(0, 1)]
    public float obstaclePercent = 0.25f;

    private Texture2D noiseTexture;
    private Color[] pixels;
    private Renderer rend;

    private void Start()
    {
        Map.SetSize(mapSize.x, mapSize.y);
        rend = GetComponent<Renderer>();
        rend.enabled = renderTexture;
        if (renderTexture)
        {
            noiseTexture = new Texture2D(Map.Width, Map.Height);
            pixels = new Color[Map.Width * Map.Height];
            rend.sharedMaterial.mainTexture = noiseTexture;
        }
        GenerateNewMap();
    }

    //---------------------------------------------------------------------------------------------------
    // Define all the coords to the world
    //---------------------------------------------------------------------------------------------------
    private void GenerateNewMap()
    {
        xOffset = Random.Range(-168, 82);
        yOffset = Random.Range(-168, 82);        

        float xCoord, yCoord, sample;
        for (int x = 0; x < Map.Width; x++)
        {
            for (int y = 0; y < Map.Height; y++)
            {
                xCoord = xOffset + (float)x / (float)Map.Width * scale;
                yCoord = yOffset + (float)y / (float)Map.Height * scale;

                sample = Mathf.PerlinNoise(xCoord, yCoord);
                sample = RoundSample(sample);
                if (renderTexture)
                    pixels[x * Map.Width + y] = new Color(sample, sample, sample);
                Map.AddCoord(x, y, sample == 0);
            }
        }

        if (renderTexture)
            RenderMap();
        if (buildMap)
            CreateMap();

        Debug.Log("REMOVE THIS LINE!!");
        FindObjectOfType<PlayerController>().transform.position = new Vector3(Map.SpawnPoint.x, 0, Map.SpawnPoint.y);
    }

    //----------------------------------------------------------------------------------------------------
    // Render the texture of the map
    //----------------------------------------------------------------------------------------------------
    public void RenderMap()
    {
        noiseTexture.SetPixels(pixels);
        noiseTexture.Apply();
    }

    //---------------------------------------------------------------------------------------------------
    // Place the obstacles in the world
    //---------------------------------------------------------------------------------------------------
    private void CreateMap()
    {
        Transform currentT = null;
        foreach (Map.Coord c in Map.Coords)
        {
            currentT = null;
            if (c.isDoor)
                currentT = doorT;
            else if (c.isBorder)
                currentT = borderT;
            else if (c.isObstacle)
                currentT = obstacleT;

            if (currentT != null)
                Instantiate(currentT, new Vector3(c.x, 0, c.y), Quaternion.identity, transform);
        }
    }

    //---------------------------------------------------------------------------------------------------
    // Round the sample to the correct color depending on size
    //---------------------------------------------------------------------------------------------------
    private int RoundSample(float sample)
    {
        return (sample > obstaclePercent) ? 1 : 0;
    }
}