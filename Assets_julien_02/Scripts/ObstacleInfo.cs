using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInfo {
    private int x, y;
    private bool isObstacle, isBorder, isDoor;
    private Obstacle obstacleObject;

    public int X { get { return x; } }
    public int Y { get { return y; } }
    public bool IsObstacle { get { return isObstacle; } }
    public bool IsBorder { get { return isBorder; } }
    public bool IsDoor { get { return isDoor; } }

    public Vector3 Position { get { return obstacleObject.transform.position; } }

    public ObstacleInfo(int _x, int _y, bool _isObstacle, bool _isBorder, bool _isDoor = false)
    {
        x = _x;
        y = _y;
        isObstacle = _isObstacle;
        isBorder = _isBorder;
        isDoor = _isDoor;
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
