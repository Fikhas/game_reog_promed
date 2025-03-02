using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveObject : MonoBehaviour
{
    [SerializeField] GameObject objectToInactive;

    public void InactiveObjectMethod()
    {
        objectToInactive.SetActive(false);
    }
}
