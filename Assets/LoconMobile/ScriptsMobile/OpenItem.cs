using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenItem : MonoBehaviour
{
    [SerializeField]
    Signal openItemSignal;

    public void OpenItemFunc()
    {
        openItemSignal.Raise();
    }
}
