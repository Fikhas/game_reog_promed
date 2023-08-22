using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomMove : MonoBehaviour
{
    // public Vector2 cameraChange;
    // public Vector3 playerChange;
    // public CameraMovement cam;
    public GameObject placeText;
    public TMP_Text textPlace;
    public SpawnPoint spawnPoint;
    public Transform spawnCordinat;
    public GameObject flowchart;
    public GameObject enemy;
    public Vector2 enemyPos;
    public Signal spawnSignal;
    public GameObject[] tileToRemove;
    public Signal signalToRaise;
    public GameStates gameStates;
    public static RoomMove sharedInstance;
    public string textName;
    public int enemyAmount;
    public bool isNeedDialog;
    public bool isNeedText;
    public bool isNeedEnemy;
    private bool isSpawn;
    public bool isEntered;
    public bool isClose;
    public bool isCloseWhenEnter;

    // Start is called before the first frame update
    void Start()
    {
        // cam = Camera.main.GetComponent<CameraMovement>();
        sharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawn)
        {
            if (flowchart == null || !flowchart.activeInHierarchy)
            {
                for (int i = 0; i < enemyAmount; i++)
                {
                    Instantiate(enemy, enemyPos, Quaternion.identity);
                    if (i < enemyAmount)
                    {
                        isSpawn = false;
                    }
                }
            }
        }

        if (!isClose)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            if (tileToRemove != null)
            {
                for (int i = 0; i < tileToRemove.Length; i++)
                {
                    tileToRemove[i].SetActive(false);
                }
            }
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            if (tileToRemove != null)
            {
                for (int i = 0; i < tileToRemove.Length; i++)
                {
                    tileToRemove[i].SetActive(true);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            // cam.maxPosition += cameraChange;
            // cam.minPosition += cameraChange;
            // other.transform.position += playerChange;
            spawnPoint.runtimeSpawnCordinat = spawnCordinat.position;
            if (isNeedText && !isEntered)
            {
                StartCoroutine(placeNameCo());
            }
            if (isNeedDialog && !isEntered)
            {
                flowchart.SetActive(true);
            }
            if (isNeedEnemy && !isEntered)
            {
                spawnSignal.Raise();
            }
            if (isCloseWhenEnter && !isEntered)
            {
                if (!isClose)
                {
                    isClose = true;
                }
            }
            // if (gameStates != null)
            // {
            //     gameStates.chessSignal = signalToRaise;
            // }
            isEntered = true;
        }
    }

    private IEnumerator placeNameCo()
    {
        placeText.SetActive(true);
        textPlace.text = textName;
        yield return new WaitForSeconds(4f);
        placeText.SetActive(false);
    }

    public void EnemySpawn()
    {
        isSpawn = true;
    }

    public void ChangeClose()
    {
        isClose = false;
    }
}
