using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private float fixedZ; // Stores the Z position to keep it constant

    void Start()
    {
        fixedZ = transform.position.z; // Set initial Z position
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = fixedZ; // Lock the Z position
        transform.position = pos; // Apply the constraint
    }
}
