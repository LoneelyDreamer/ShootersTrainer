using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<Road> roads;
    [SerializeField] private Transform bluTarget;
    [SerializeField] private Transform purpleTarget;

    private float spawnTimer = 1f;
    private void Update()
    {
        SpawnTarget();
    }

    private void SpawnTarget()
    {
        if (!GameManager.Instance.IsGamePlaying()) { return; }
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            int i = Random.Range(0, spawnPoints.Count); 
            if (roads[i].CheckIsAvailable()) 
            {
                Instantiate(RandomTargetToSpawn(), spawnPoints[i].position, Quaternion.identity);
            }

            spawnTimer = Random.Range(0.4f, 1.2f);
        }
    }

    private Transform RandomTargetToSpawn() 
    {
        int prosent = 80;
        if (Random.Range(0, 101) <= prosent)
        {
            return purpleTarget;
        }
        else
        {
            return bluTarget;
        }
    }
}
