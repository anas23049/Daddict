using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsManager : MonoBehaviour
{
    public GameObject[] drugs;
    public GameObject player;  // Assign the player GameObject in the Inspector
    public GameObject firstaid;
    public GameObject coin;
    public GameObject logenemy;
    public GameObject life;
    public GameObject[] booster;
    void Awake()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
    void Start()
    {
        
        InvokeRepeating("Spawn", 2f, 3f); // drugs
        InvokeRepeating("Spawn1", 10f, 20f); // first Aid
        InvokeRepeating("Spawn2", 1f, 2f); // coin
        InvokeRepeating("Spawn3", 3f, 5f); //logenemy
        InvokeRepeating("Spawn4", 20f, 25f); //lifee
        InvokeRepeating("Spawn5", 20f, 25f); //boasster
    }

    void Spawn()
    {
        Vector3 playerPos = player.transform.position;

        int x = Random.Range((int)playerPos.x + 30, (int)playerPos.x + 81);
        int y = Random.Range(1, 1);
        float z = 12f;

        Vector3 position = new Vector3(x, y, z);

        int randomIndex = Random.Range(0, drugs.Length);
        Instantiate(drugs[randomIndex], position, Quaternion.identity);
    }
    void Spawn1()
    {
        Vector3 playerPos = player.transform.position;

        int x = Random.Range((int)playerPos.x + 30, (int)playerPos.x + 81);
        int y = Random.Range(1, 1);
        float z = 12f;

        Vector3 position = new Vector3(x, y, z);

        
        Instantiate(firstaid, position, Quaternion.identity);
    }
    void Spawn2()
    {
        Vector3 playerPos = player.transform.position;

        int x = Random.Range((int)playerPos.x + 10, (int)playerPos.x + 81);
        int y = Random.Range(1, 1);
        float z = 12f;

        Vector3 position = new Vector3(x, y, z);


        Instantiate(coin, position, Quaternion.identity);
    }
    void Spawn3()
    {
        Vector3 playerPos = player.transform.position;

        int x = Random.Range((int)playerPos.x + 10, (int)playerPos.x + 81);
        int y = Random.Range(1, 1);
        float z = 12f;

        Vector3 position = new Vector3(x, y, z);


        Quaternion rotation = Quaternion.Euler(0f, -90f, 0f);

        Instantiate(logenemy, position, rotation);

    }
    void Spawn4()
    {
        Vector3 playerPos = player.transform.position;

        int x = Random.Range((int)playerPos.x + 10, (int)playerPos.x + 81);
        int y = Random.Range(1, 1);
        float z = 12f;

        Vector3 position = new Vector3(x, y, z);


        Instantiate(life, position, Quaternion.identity);
    }
    void Spawn5()
    {
        Vector3 playerPos = player.transform.position;

        int x = Random.Range((int)playerPos.x + 30, (int)playerPos.x + 81);
        int y = Random.Range(1, 1);
        float z = 12f;

        Vector3 position = new Vector3(x, y, z);

        int randomIndex = Random.Range(0, booster.Length);
        Instantiate(booster[randomIndex], position, Quaternion.identity);
    }
}
