using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField] Vector3 cameraPosStage;
    private CameraMovement camm;

    private void Start()
    {
        camm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camm.cameraPosition = cameraPosStage;
        }
    }
}
