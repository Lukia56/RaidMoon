using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMFadeOut : MonoBehaviour
{
    [SerializeField]
    private float fadeOutSpeed;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Player player;

    private void Update()
    {
        if (player.IsDead)
        {
            audioSource.pitch = Mathf.MoveTowards(audioSource.pitch, 0, fadeOutSpeed * Time.deltaTime);
        }
    }
}
