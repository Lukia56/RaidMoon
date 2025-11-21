using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderKatanaAttack : ColliderParent
{
    // ƒqƒbƒgˆ—
    protected override void HitToTarget(GameObject hitObject)
    {
        //Debug.Log("“UŒ‚ˆ—");

        // “G‚ª€–S‚µ‚½‚ç©g‚ğíœ
        if (hitObject.GetComponent<EnemyMelee>().Dead())
        {
            Destroy(gameObject);
        }
    }
}
