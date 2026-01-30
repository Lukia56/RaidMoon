using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMeleeEnemyAttack : ColliderParent
{
    [SerializeField]
    private bool isIgnoreInv;   // –³“GŠÔ‚ğ–³‹‚·‚é‚©‚Ç‚¤‚©

    // ƒqƒbƒgˆ—
    protected override void HitToTarget(GameObject hitObject)
    {
        //Debug.Log("‹ßÚ“G UŒ‚ˆ—");

        var component = hitObject.GetComponent<Player>();

        if (component)
        {
            if (!isIgnoreInv && component.IsInvincible) return;

            component.Dead();
        }

        Destroy(gameObject);
    }
}
