using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogSpawner : MonoBehaviour
{
    [SerializeField] GameObject dialogToSpawn;
    [SerializeField] GameObject dialogToIn;

    public void SpawnDialog()
    {
        GameObject dialogToShow = Instantiate(dialogToSpawn, this.transform.position, Quaternion.identity);
        dialogToShow.GetComponent<PlayDialog>().dialogToActive = dialogToIn;
    }
}
