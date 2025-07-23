using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class logenemy : MonoBehaviour
{
    public float moveSpeed = 0.6f; // Speed of movement
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(-moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
    }

    void FixedUpdate()
    {
        // Maintain constant velocity in -X direction
        rb.linearVelocity = new Vector3(-moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
    }
}
