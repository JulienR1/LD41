using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfo {

    public enum ObstacleType { AIR, OBSTACLE, BORDER, DOOR, SHOPPING_CART }

    private int x, y;
    private ObstacleType obstacleType;

    public int X { get { return x; } }
    public int Y { get { return y; } }
    public ObstacleType Type { get { return obstacleType; } }

    public Vector3 Position { get { return new Vector3(X, 0.5f, Y); } }

    public ObstacleInfo(int _x, int _y, ObstacleType _obstacleType) 
    {
        x = _x;
        y = _y;
        obstacleType = _obstacleType;
    }

    public override string ToString()
    {
        return x + " " + y;
    }
}
