using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Fikhas.Audio
{
    public class SoundSystem : MonoBehaviour
    {
        public static SoundSystem Instance;

        [SerializeField]
        private SoundPool audioPool;
        [SerializeField]
        private SoundDatabaseSO audioDatabaseSO;

        private Coroutine stopSoundCo;

        private void Awake()
        {
            Instance = this;
        }

        public void PlayAudio(string audioName, bool isLoop, string audioIdentifier, GameObject obj = null)
        {
            // Get sound object pool
            GameObject objectReceived = audioPool.GetObject();
            if (obj != null)
            {
                objectReceived.transform.SetParent(obj.transform);
                objectReceived.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
                objectReceived.GetComponent<AudioSource>().spatialBlend = 1;
            }
            else
            {
                objectReceived.GetComponent<AudioSource>().spatialBlend = 0;
            }

            // Get AudioClip from sound database
            AudioClip audioClip = audioDatabaseSO.GetAudioClip(audioName);
            Debug.Log("Sound Clip is: " + audioClip);

            // Assign an audio clip into AudioSource component in objectReceived
            if (isLoop)
            {
                objectReceived.GetComponent<AudioSource>().loop = true;
            }
            else
            {
                objectReceived.GetComponent<AudioSource>().loop = false;
                if (obj != null)
                {
                    StartCoroutine(StopAudioCO(audioIdentifier, audioClip.length, obj));
                }
                else
                {
                    StartCoroutine(StopAudioCO(audioIdentifier, audioClip.length));
                }
            }
            objectReceived.GetComponent<AudioSource>().clip = audioClip;
            objectReceived.GetComponent<AudioSource>().Play();
            objectReceived.name = audioIdentifier;
        }

        public void StopAudio(string audioIdentifier, GameObject obj = null)
        {
            AudioSource[] sounds;
            if (obj != null)
            {
                sounds = obj.GetComponentsInChildren<AudioSource>();
            }
            else
            {
                sounds = audioPool.gameObject.GetComponentsInChildren<AudioSource>();
            }
            foreach (var sound in sounds)
            {
                if (sound.gameObject.name == audioIdentifier)
                {
                    if (obj != null)
                    {
                        sound.gameObject.transform.SetParent(audioPool.gameObject.transform);
                    }
                    audioPool.ReturnObject(sound.gameObject);
                    return;
                }
            }
        }

        private IEnumerator StopAudioCO(string audioIdentifier, float soundDuration, GameObject obj = null)
        {
            yield return new WaitForSeconds(soundDuration);
            Debug.Log($"entered stop audio coroutine");
            if (obj != null)
            {
                Debug.Log("audio has set to new parent");
                StopAudio(audioIdentifier, obj);
            }
            else
            {
                StopAudio(audioIdentifier);
            }
        }
    }
}
