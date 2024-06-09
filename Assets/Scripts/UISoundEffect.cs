using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundEffect : MonoBehaviour
{
    [SerializeField] GameObject buttonSoundEffect;

    public void PlaySoundEffect()
    {
        Instantiate(buttonSoundEffect);
    }
}
