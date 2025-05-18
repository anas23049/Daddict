using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs;
    public float spawnRate = 2f;
    public float minY = -2f, maxY = 2f;
    public float spawnDistanceAhead = 20f;

    private Transform player;
    private float timer = 0f;
    private GameObject lastSpawnedObstacle; // 👈 reference to previous

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Assign the 'Player' tag to your player GameObject.");
            enabled = false;
        }
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0f;
        }

        
    }

   public float obstacleLifetime = 10f; // Time in seconds before destroying
private List<GameObject> activeObstacles = new List<GameObject>();

void SpawnObstacle()
{
    if (obstaclePrefabs.Count == 0 || player == null) return;

    Vector3 spawnPos = player.position + Vector3.right * spawnDistanceAhead;
    spawnPos.y = 1f;
    spawnPos.z = player.position.z;

    GameObject obstacle = Instantiate(
        obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)],
        spawnPos,
        Quaternion.identity
    );

    // Rotate to face the player
    Vector3 lookDir = player.position - spawnPos;
    lookDir.y = 0;
    obstacle.transform.rotation = Quaternion.LookRotation(lookDir);

    // Add to list and schedule for destruction
    activeObstacles.Add(obstacle);
    Destroy(obstacle, obstacleLifetime);
}
}