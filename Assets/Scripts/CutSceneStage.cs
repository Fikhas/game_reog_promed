using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class CutSceneStage : MonoBehaviour
{
    [SerializeField] GameObject cutSceneToManage;
    [SerializeField] Signal cutSceneOnSignal;
    [SerializeField] Signal cutSeneOffSignal;
    private bool isRaiseOffSignal;

    private void Update()
    {
        if (!cutSceneToManage.activeInHierarchy && isRaiseOffSignal)
        {
            cutSeneOffSignal.Raise();
            isRaiseOffSignal = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.GetComponent<StageManagement>().isEntered)
        {
            if (other.CompareTag("Player"))
            {
                cutSceneToManage.SetActive(true);
                cutSceneOnSignal.Raise();
                isRaiseOffSignal = true;
            }
        }
    }
}
