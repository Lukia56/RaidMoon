using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderParry : ColliderParent
{
    [SerializeField]
    private int _direction;
    public int Direction { set { _direction = value; } }

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

        // “G‚Ì•û‚ğŒü‚¢‚Ä‚¢‚È‚©‚Á‚½‚ç¸”s
        if (_direction == enemy.Direction) return;

        if (!enemy.Parried()) return;

        audioSource.PlayOneShot(seParry);

        Destroy(gameObject);
    }
}
