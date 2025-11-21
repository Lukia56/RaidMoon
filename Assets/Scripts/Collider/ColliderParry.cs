using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class ColliderParry : ColliderParent
{
    // ƒqƒbƒgˆ—
    protected override void HitToTarget(GameObject hitObject)
    {
        //Debug.Log("‚Í‚¶‚«ˆ—");
        hitObject.GetComponent<Enemy>().Parried();

        Destroy(gameObject);
    }
}
