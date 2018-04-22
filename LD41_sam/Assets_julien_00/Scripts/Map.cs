using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Map
{
    private static Vector2Int mapSize;
    private static Coord[] map;

    public static int Width { get { return mapSize.x; } }
    public static int Height { get { return mapSize.y; } }
    public static Coord[] Coords { get { return map; } }

    public static void SetSize(int x, int y)
    {
        mapSize = new Vector2Int(x, y);
        map = new Coord[x * y];
    }

    public static void AddCoord(int x, int y, bool b)
    {
        bool isBorder = (x == 0 || y == 0 || x == Width - 1 || y == Height - 1);
        map[x * Width + y] = new Coord(x, y, b, isBorder);
    }

    public class Coord
    {
        public int x, y;
        public bool isObstacle, isBorder;

        public Coord(int _x, int _y, bool _isObstacle, bool _isBorder)
        {
            x = _x;
            y = _y;
            isObstacle = _isObstacle;
            isBorder = _isBorder;
        }
    }
}
