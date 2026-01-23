using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderParry : ColliderParent
{
    private AudioSource audioSource;
    // UŒ‚‚ÌSE
    [SerializeField]
    private AudioClip seParry;

    private void Awake()
    {
        audioSource = transform.parent.GetComponent<AudioSource>();
    }

    // ƒqƒbƒgˆ—
    protected override void HitToTarget(GameObject hitObject)
    {
        //Debug.Log("‚Í‚¶‚«ˆ—");

        Enemy enemy = hitObject.GetComponent<Enemy>();

        if (!enemy.Parried()) return;

        audioSource.PlayOneShot(seParry);

        Destroy(gameObject);
    }
}
