using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UIActiveDelay : MonoBehaviour
{
    [SerializeField] GameObject cutSceneToManage;
    [SerializeField] Signal UIActiveSignal;
    private bool isRaiseOffSignal;

    private void Update()
    {
        if (!cutSceneToManage.activeInHierarchy && isRaiseOffSignal)
        {
            UIActiveSignal.Raise();
            isRaiseOffSignal = false;
        }
    }

    public void LetSignalRaise()
    {
        StartCoroutine(SignalRaiseCo());
    }

    private IEnumerator SignalRaiseCo()
    {
        yield return new WaitForSeconds(3f);
        isRaiseOffSignal = true;
    }
}
