using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Map {

    private static Vector2Int mapSize;

    public static int Width { get { return mapSize.x; } }
    public static int Height { get { return mapSize.y; } }

    public static void SetSize(int x, int y)
    {
        mapSize = new Vector2Int(x, y);
    }
    
}
