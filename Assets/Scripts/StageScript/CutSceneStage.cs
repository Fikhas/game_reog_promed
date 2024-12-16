using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class CutSceneStage : MonoBehaviour
{
    [SerializeField] GameObject cutSceneToManage;
    [SerializeField] Signal cutSceneOnSignal;
    [SerializeField] Signal cutSeneOffSignal;
    [SerializeField] Signal[] additionalSignal;
    [SerializeField] bool isPlayWithTrigger;
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
            if (additionalSignal != null)
            {
                foreach (Signal signal in additionalSignal)
                {
                    signal.Raise();
                }
            }
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
                if (isPlayWithTrigger)
                {
                    player.GetComponent<PlayerMovement>().playerCurrentState = PlayerState.interact;
                    StartCoroutine(ManualCutSceneCo(1f));
                }
            }
        }
    }

    public void ManualCutScene()
    {
        if (gameObject.GetComponent<StageManagement>().isAnyPlayer)
        {
            Debug.Log("Manual Cut Scene Start");
            StartCoroutine(ManualCutSceneCo(0.1f));
        }
    }

    private IEnumerator ManualCutSceneCo(float waitTime)
    {
        yield return new WaitForSeconds(0.1f);
        cutSceneOnSignal.Raise();
        yield return new WaitForSeconds(waitTime);
        cutSceneToManage.SetActive(true);
        isRaiseOffSignal = true;
    }
}
