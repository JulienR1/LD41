using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Map
{
    private const float SPAWN_LIMIT = 3.0f;
    private const float CHANCE_TO_SPAWN = 0.4f;

    private static Vector2Int mapSize;
    private static Coord[] map;
    private static Coord spawnPoint;
    private static Coord door;

    public static int Width { get { return mapSize.x; } }
    public static int Height { get { return mapSize.y; } }
    public static Coord[] Coords { get { return map; } }
    public static Coord SpawnPoint { get { return spawnPoint; } }
    public static Coord Door { get { return door; } }

    public static void SetSize(int x, int y)
    {
        mapSize = new Vector2Int(x, y);
        map = new Coord[x * y];
    }

    //---------------------------------------------------------------------------------------------------
    // Add a new coordinate to the map
    // Define the spawn point of the world
    //---------------------------------------------------------------------------------------------------
    public static void AddCoord(int x, int y, bool b)
    {
        bool isBorder = (x == 0 || y == 0 || x == Width - 1 || y == Height - 1);
        Coord currentCoord = new Coord(x, y, b, isBorder);
        map[x * Width + y] = currentCoord;

        // Define the spawn point
        if (spawnPoint == null)
        {
            if (!b && x > 1 / SPAWN_LIMIT * Width && x < (SPAWN_LIMIT - 1) / SPAWN_LIMIT * Width && (y == 1 || y == Height - 1))
            {
                if (Random.value < CHANCE_TO_SPAWN || x > (SPAWN_LIMIT - 1) / SPAWN_LIMIT * Width)
                {
                    spawnPoint = currentCoord;
                    if (y == 1)
                    {
                        currentCoord = map[x * Width + ((y == 1) ? 0 : Height)];
                        currentCoord.isDoor = true;
                        door = currentCoord;
                    }
                }
            }
        }
    }

    public class Coord
    {
        public int x, y;
        public bool isObstacle, isBorder, isDoor;

        public Coord(int _x, int _y, bool _isObstacle, bool _isBorder)
        {
            x = _x;
            y = _y;
            isObstacle = _isObstacle;
            isBorder = _isBorder;
            isDoor = false;
        }
    }
}
