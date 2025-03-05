using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public Transform target; 
    //public Vector3 offset = new Vector3(0, 2, -4); 
    //public float smoothSpeed = 5f; 

    //public float rotationSpeed = 3f;
    //private float currentX = 0f;
    //private float currentY = 0f;
    //public float minY = -20f, maxY = 60f;

    //public LayerMask collisionMask;

    //private void LateUpdate()
    //{
    //    if (!target) return;


    //    currentX += Input.GetAxis("Mouse X") * rotationSpeed;
    //    currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
    //    currentY = Mathf.Clamp(currentY, minY, maxY);


    //    Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);


    //    Vector3 desiredPosition = target.position + rotation * offset;


    //    if (Physics.Linecast(target.position, desiredPosition, out RaycastHit hit, collisionMask))
    //    {
    //        desiredPosition = hit.point;  
    //    }


    //    transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    //    transform.LookAt(target.position + Vector3.up * 1.5f); 
    //}
    public Transform player;
    public Vector3 offset = new Vector3(-5f, 2f, 0f);
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (!player) return;

        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(10f, 90f, 0f);
    }

}
