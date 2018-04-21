using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfo {

    public enum ObstacleType { AIR, OBSTACLE, BORDER, DOOR, SHOPPING_CART }

    private int x, y;
    private ObstacleType obstacleType;
    private bool isObstacle, isBorder, isDoor;
    private Obstacle obstacleObject;

    public int X { get { return x; } }
    public int Y { get { return y; } }
    public ObstacleType Type { get { return obstacleType; } }

    public Vector3 Position { get { return obstacleObject.transform.position; } }

    public ObstacleInfo(int _x, int _y, ObstacleType _obstacleType) 
    {
        x = _x;
        y = _y;
        obstacleType = _obstacleType;
    }

    public void SetObstacleObject(Obstacle t)
    {
        obstacleObject = t;
    }

    public void SetVisibility(bool isVisible)
    {
        obstacleObject.SetVisibility(isVisible);
    }

    public override string ToString()
    {
        return x + " " + y;
    }
}
