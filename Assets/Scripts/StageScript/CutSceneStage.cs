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
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

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
                if (gameObject.GetComponent<DialogSceneManagement>() == null)
                {
                    player.GetComponent<PlayerMovement>().playerCurrentState = PlayerState.interact;
                    StartCoroutine(ManualCutSceneCo(1f));
                }
            }
        }
    }

    public void ManualCutScene()
    {
        player.GetComponent<PlayerMovement>().playerCurrentState = PlayerState.interact;
        StartCoroutine(ManualCutSceneCo(2f));
    }

    private IEnumerator ManualCutSceneCo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        cutSceneToManage.SetActive(true);
        cutSceneOnSignal.Raise();
        isRaiseOffSignal = true;
    }
}
