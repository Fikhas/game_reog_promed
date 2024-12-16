using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothing;
    public Vector3 cameraPosition;

    void LateUpdate()
    {
        if (transform.position != cameraPosition)
        {
            Vector3 targetPosition = new Vector3(cameraPosition.x, cameraPosition.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
