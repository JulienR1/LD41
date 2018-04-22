using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour {

    public Enemy enemy;

    public float spawnTime = 0;

    private Vector3 spawnPosition;

	
	void Update () {

        InvokeRepeating("Spawn", spawnTime, spawnTime);
		
	}
	
	void Spawn () {

        spawnPosition.x = Random.Range(0, Map.Width);

        spawnPosition.y = Map.Height;

        spawnPosition.z = Random.Range(0, Map.Width);

        if (Map.GetFromPosition(spawnPosition).Type == ObstacleInfo.ObstacleType.AIR) {
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }

        



    }
}
