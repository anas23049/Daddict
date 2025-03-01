using UnityEngine;

public class ground : MonoBehaviour
{
    public float resetPosition;     // X position where background resets

    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.left * 1f * Time.deltaTime;

        // If it moves past resetPosition, move it back
        if (transform.position.x < resetPosition)
        {
            transform.position = startPosition;
        }
    }
}
