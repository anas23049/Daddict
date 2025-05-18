using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject sectionPrefab;
    public float sectionLength = 300f;
    public float destroyDelay = 7f;
    public GameObject firstterrain;
    private static float nextXPosition = 100f; 
    private const float fixedZ = 8f;
    private const float fixedY = 0f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

           
            nextXPosition += sectionLength;
            Vector3 spawnPos = new Vector3(nextXPosition, fixedY, fixedZ);

           
            Instantiate(sectionPrefab, spawnPos, Quaternion.identity);

          
            Destroy(transform.parent.gameObject, destroyDelay);
            Destroy(firstterrain,1);
        }
    }
}
