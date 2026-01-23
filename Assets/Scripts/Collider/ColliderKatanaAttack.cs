using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderKatanaAttack : ColliderParent
{
    private AudioSource audioSource;
    // 攻撃のSE
    [SerializeField]
    private AudioClip seAttack;

    private void Awake()
    {
        audioSource = transform.parent.GetComponent<AudioSource>();
    }

    // ヒット処理
    protected override void HitToTarget(GameObject hitObject)
    {
        //Debug.Log("刀攻撃処理");

        EnemyMelee component = hitObject.GetComponent<EnemyMelee>();

        // EnemyMeleeコンポーネントを持っていないなら処理を行わない
        if (!component) return;

        // 敵が死亡したら自身を削除
        if (component.Dead())
        {
            audioSource.PlayOneShot(seAttack);

            Destroy(gameObject);
        }
    }
}
