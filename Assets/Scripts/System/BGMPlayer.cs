using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField]
    private float myVolume;

    [SerializeField]
    AudioSource audioSource;

    void Update()
    {
        audioSource.volume = (1.0f - Fader.GetAlpha()) * myVolume;
    }
}
