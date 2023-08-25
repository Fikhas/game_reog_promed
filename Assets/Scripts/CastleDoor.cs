using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDoor : MonoBehaviour
{
    [SerializeField] GameObject castleDoorClose;
    [SerializeField] GameObject castleDoorOpen;

    private void Start()
    {
        castleDoorClose.SetActive(true);
        castleDoorOpen.SetActive(false);
    }

    public void openCastleDoor()
    {
        castleDoorClose.SetActive(false);
        castleDoorOpen.SetActive(true);
    }
}
