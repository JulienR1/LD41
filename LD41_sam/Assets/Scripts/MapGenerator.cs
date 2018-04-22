using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform floorT;
    public Transform obstacleT, borderT, doorT, shoppingCartT;

    [Header("Settings")]
    public Vector2Int mapSize;
    public bool renderTexture, buildMap;
    [Range(0, 1)]
    public float obstaclePercent = 0.25f;
    [Range(0, 1)]
    public float shoppingCartPercent = 0.05f;

    [Header("Perlin noise settings")]
    public float scale = 1;

    private float xOffset, yOffset;
    private Texture2D noiseTexture;
    private Color[] pixels;
    private Renderer rend;

    private void Start()
    {
        Map.Initialize(mapSize.x, mapSize.y, shoppingCartPercent);
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
                Map.AddCoord(x, y, sample == 1);
            }
        }

        if (renderTexture)
            RenderMap();
        if (buildMap)
            CreateMap();

        Debug.Log("REMOVE THIS LINE!!");
        FindObjectOfType<PlayerController>().transform.position = new Vector3(Map.SpawnPoint.X, 0, Map.SpawnPoint.Y);
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
        ObstacleInfo currentObstacleInfo = null;
        for (int i = 0; i < Map.CoordMap.Length; i++)
        {
            currentObstacleInfo = Map.CoordMap[i];
            switch (currentObstacleInfo.Type)
            {
                case ObstacleInfo.ObstacleType.OBSTACLE: currentT = obstacleT; break;
                case ObstacleInfo.ObstacleType.BORDER: currentT = borderT; break;
                case ObstacleInfo.ObstacleType.DOOR: currentT = doorT; break;
                case ObstacleInfo.ObstacleType.SHOPPING_CART: currentT = shoppingCartT; break;
                default: currentT = null; break;
            }

            if (currentT != null)
            {
                Transform t = Instantiate(currentT, new Vector3(Map.CoordMap[i].X, 0, Map.CoordMap[i].Y), Quaternion.identity, transform);
                Map.CoordMap[i].SetObstacleObject(t.GetComponent<Obstacle>());
            }
        }

        floorT.localScale = new Vector3(Map.Width / 10, 1, Map.Height / 10);
        floorT.localPosition = new Vector3(Map.Width / 2 - 0.5f, -0.5f, Map.Height / 2 - 0.5f);
    }

    //---------------------------------------------------------------------------------------------------
    // Round the sample to the correct color depending on size
    //---------------------------------------------------------------------------------------------------
    private int RoundSample(float sample)
    {
        return (sample > obstaclePercent) ? 1 : 0;
    }
}