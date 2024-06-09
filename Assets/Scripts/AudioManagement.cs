using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    [SerializeField] GameObject audioToPlay;
    [SerializeField] bool isPlayWithTrigger;
    GameObject audioPlayed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayWithTrigger)
        {
            if (gameObject.GetComponent<StageManagement>().isAnyPlayer)
            {
                if (other.CompareTag("Player"))
                {
                    GameObject[] audiosToDestroy = GameObject.FindGameObjectsWithTag("Audio");
                    if (audiosToDestroy != null)
                    {
                        foreach (GameObject audioToDestroy in audiosToDestroy)
                        {
                            Destroy(audioToDestroy);
                        }
                    }
                    audioPlayed = Instantiate(audioToPlay, this.transform.position, Quaternion.identity);
                }
            }
        }
    }

    public void playAudio()
    {
        if (gameObject.GetComponent<StageManagement>().isAnyPlayer)
        {
            GameObject[] audiosToDestroy = GameObject.FindGameObjectsWithTag("Audio");
            if (audiosToDestroy != null)
            {
                foreach (GameObject audioToDestroy in audiosToDestroy)
                {
                    Destroy(audioToDestroy);
                }
            }
            audioPlayed = Instantiate(audioToPlay, this.transform.position, Quaternion.identity);
        }
    }

    public void stopAudio()
    {
        if (audioPlayed != null)
        {
            Destroy(audioPlayed);
        }
    }
}
