using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DoorManagement : MonoBehaviour
{
    [SerializeField] bool isClose;
    [SerializeField] bool isCloseWhenEnter;
    [SerializeField] GameObject doorToManage;
    private void Update()
    {
        if (doorToManage != null)
        {
            if (isClose)
            {
                doorToManage.SetActive(true);
            }
            else
            {
                doorToManage.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.GetComponent<StageManagement>().isEntered)
        {
            if (other.CompareTag("Player"))
            {
                if (isCloseWhenEnter)
                {
                    isClose = true;
                }
            }
        }
    }

    public void ChangeCloseToTrue()
    {
        isClose = true;
    }

    public void ChangeCloseToFalse()
    {
        isClose = false;
    }
}
