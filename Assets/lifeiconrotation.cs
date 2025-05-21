using UnityEngine;

public class lifeiconrotation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0); // Rotation speed in degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
