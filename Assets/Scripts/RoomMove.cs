using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    public CameraMovement cam;
    public bool needText;
    public string textName;
    public GameObject placeText;
    public TMP_Text textPlace;
    public SpawnPoint spawnPoint;
    public Transform spawnCordinat;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        // gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (!enemy.activeInHierarchy)
        // {
        //     gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        //     // collisionDua.SetActive(false);
        // }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.maxPosition += cameraChange;
            cam.minPosition += cameraChange;
            other.transform.position += playerChange;
            spawnPoint.runtimeSpawnCordinat = spawnCordinat.position;
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        placeText.SetActive(true);
        textPlace.text = textName;
        yield return new WaitForSeconds(4f);
        placeText.SetActive(false);
    }
}
