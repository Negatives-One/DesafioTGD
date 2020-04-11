using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float moveSpeed;
    public Transform target; // Drop the player in the inspector of the camera

    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }
}
