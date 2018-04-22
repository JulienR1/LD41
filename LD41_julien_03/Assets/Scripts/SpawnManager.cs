using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Enemy enemy;
    public float spawnTime = 0;

    public Vector2Int minMaxEnemyCount;
    private int finalEnemyCount, spawnedCount;

    private Vector3 spawnPosition;

    private void Start()
    {
        finalEnemyCount = Random.Range(minMaxEnemyCount.x, minMaxEnemyCount.y + 1);
        Spawn();
    }

    private void Spawn()
    {
        while (spawnedCount < finalEnemyCount)
        {
            spawnPosition.x = Random.Range(0, Map.Width);
            spawnPosition.y = 0.7f;
            spawnPosition.z = Random.Range(0, Map.Height);

            if (Map.GetFromPosition(spawnPosition).Type == ObstacleInfo.ObstacleType.AIR)
            {
                Instantiate(enemy, spawnPosition, Quaternion.identity, transform);
                spawnedCount++;
            }
        }
    }
}