using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBowAttack : MonoBehaviour
{
    [SerializeField] private float m_LifeCounter;           // 破壊されるまでのカウンタ
    [SerializeField] private string m_TargetTagName;        // 対象のタグ名
    // チャージ済みかどうか
    [SerializeField] bool _isCharged;
    public bool IsCharged { get => _isCharged; set => _isCharged = value; }
    // 処理が終わっているかどうか
    [SerializeField] bool _isDead;
    public bool IsDead { get => _isDead; set => _isDead = value; }

    private void Update()
    {
        // 生存処理
        ProcessLife();
    }

    // 生存処理
    private void ProcessLife()
    {
        // まだ生存しているならカウントダウン
        if (CanLiving())
        {
            m_LifeCounter -= Time.deltaTime;
        }
        // 生存していないなら破壊
        else
        {
            _isDead = true;
        }
    }

    // 生存しているか
    private bool CanLiving()
    {
        return m_LifeCounter > 0;
    }

    // 衝突処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 対象に衝突していたら
        if (collision.gameObject.tag == m_TargetTagName)
        {
            // ヒット処理
            HitToTarget(collision.gameObject);
        }
    }

    // ヒット処理
    private void HitToTarget(GameObject hitObject)
    {
        // 処理が終わっていないなら
        if (_isDead) return;

        // チャージ済みなら
        if (!_isCharged) return;

        // 敵が死亡したら自身を削除
        if (hitObject.GetComponent<EnemyMelee>().Dead())
        {
            _isDead = true;
        }
    }
}
