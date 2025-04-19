using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectiontriger : MonoBehaviour
{
    public GameObject terrain;             // Prefab to spawn
    public GameObject originalTerrain;
    public GameObject veryfirstpiece;// The first terrain piece in the scene

    public float terrainLength = 300f;
    private float nextSpawnX = 100f + 300f;

    private List<GameObject> terrainPieces = new List<GameObject>();

    private void Start()
    {
        if (originalTerrain != null)
        {
            terrainPieces.Add(originalTerrain); // Track original
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("endless"))
        {
            
            StartCoroutine(SpawnAndReplaceTerrain());
        }
    }

    private IEnumerator SpawnAndReplaceTerrain()
    {
        // Wait for physics to finish
        yield return null;

        // Destroy all previous terrain pieces
        foreach (GameObject piece in terrainPieces)
        {
            Destroy(veryfirstpiece);
            if (piece != null)
                Destroy(piece,7);
           
        }
        terrainPieces.Clear();

        // Spawn a new one
        GameObject newPiece = Instantiate(terrain, new Vector3(nextSpawnX, 0, 8), Quaternion.identity);
        terrainPieces.Add(newPiece);

        // Update for next time
        nextSpawnX += terrainLength - 100;
    }
}
