using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMeleeEnemyAttack : ColliderParent
{
    // ƒqƒbƒgˆ—
    protected override void HitToTarget(GameObject hitObject)
    {
        //Debug.Log("‹ßÚ“G UŒ‚ˆ—");

        hitObject.GetComponent<Player>().Dead();

        Destroy(gameObject);
    }
}
