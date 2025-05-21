using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class logenemy : MonoBehaviour
{
    public float moveSpeed = 0.6f; // Speed of movement
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, rb.velocity.z);
    }

    void FixedUpdate()
    {
        // Maintain constant velocity in -X direction
        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, rb.velocity.z);
    }
}
