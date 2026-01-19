using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderParry : ColliderParent
{
    // ƒqƒbƒgˆ—
    protected override void HitToTarget(GameObject hitObject)
    {
        //Debug.Log("‚Í‚¶‚«ˆ—");

        Enemy enemy = hitObject.GetComponent<Enemy>();

        if (!enemy.Parried()) return;

        Destroy(gameObject);
    }
}
