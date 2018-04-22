using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Map
{
    private const float SPAWN_LIMIT = 3.0f;
    private const float CHANCE_TO_SPAWN = 0.4f;

    private static float shoppingCartPercent;

    private static Vector2Int mapSize;
    private static ObstacleInfo[] map;
    private static ObstacleInfo spawnPoint;
    private static ObstacleInfo door;

    public static int Width { get { return mapSize.x; } }
    public static int Height { get { return mapSize.y; } }
    public static ObstacleInfo[] CoordMap { get { return map; } }
    public static ObstacleInfo SpawnPoint { get { return spawnPoint; } }
    public static ObstacleInfo Door { get { return door; } }

    public static void Initialize(int x, int y, float scPercent)
    {
        mapSize = new Vector2Int(x, y);
        map = new ObstacleInfo[x * y];
        shoppingCartPercent = scPercent;
    }

    //---------------------------------------------------------------------------------------------------
    // Return the correct object from the according position
    //---------------------------------------------------------------------------------------------------
    public static ObstacleInfo GetFromPosition(Vector3 v)
    {
        return map[(int)v.x * Width + (int)v.z];
    }

    //---------------------------------------------------------------------------------------------------
    // Add a new coordinate to the map
    // Define the spawn point of the world
    //---------------------------------------------------------------------------------------------------
    public static void AddCoord(int x, int y, bool isAir)
    {
        if (map[x * Width + y] == null)
        {
            bool isBorder = (x == 0 || y == 0 || x == Width - 1 || y == Height - 1);

            int typeValue = 0;
            if (isBorder)
                typeValue = 2;
            else if (!isAir)
                typeValue = 1;

            if (Random.value <= shoppingCartPercent)
                typeValue = (int)ObstacleInfo.ObstacleType.SHOPPING_CART;

            ObstacleInfo currentObstacleInfo = new ObstacleInfo(x, y, (ObstacleInfo.ObstacleType)typeValue);
            map[x * Width + y] = currentObstacleInfo;

            // Define the spawn point
            if (spawnPoint == null)
            {
                if (isAir && x > 1 / SPAWN_LIMIT * Width && x < (SPAWN_LIMIT - 1) / SPAWN_LIMIT * Width && (y == 1 || y == Height - 2))
                {
                    if (Random.value < CHANCE_TO_SPAWN || x > (SPAWN_LIMIT - 1) / SPAWN_LIMIT * Width)
                    {
                        spawnPoint = currentObstacleInfo;

                        // Door setup
                        currentObstacleInfo = new ObstacleInfo(x, ((y == 1) ? 0 : Height - 1), ObstacleInfo.ObstacleType.DOOR);
                        map[x * Width + ((y == 1) ? 0 : Height - 1)] = currentObstacleInfo;
                        door = currentObstacleInfo;
                    }
                }
            }
        }
    }
}