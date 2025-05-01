using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsManager : MonoBehaviour
{
    public GameObject[] drugs;
    public GameObject player;  // Assign the player GameObject in the Inspector

    void Start()
    {
        // Start spawning drugs every 3 seconds
        InvokeRepeating("Spawn", 2f, 3f);
    }

    void Spawn()
    {
        Vector3 playerPos = player.transform.position;

        int x = Random.Range((int)playerPos.x + 30, (int)playerPos.x + 81);
        int y = Random.Range(0, 3);
        float z = 12f;

        Vector3 position = new Vector3(x, y, z);

        int randomIndex = Random.Range(0, drugs.Length);
        Instantiate(drugs[randomIndex], position, Quaternion.identity);
    }
}
