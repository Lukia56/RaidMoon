using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBow : Enemy
{
    [SerializeField]
    private float _cooldownAttackCounter;   // 攻撃クールダウンのカウンタ
    [SerializeField]
    private bool _isDead;

    // パラメータ
    [SerializeField]
    Vector3 _destPosition;
    [SerializeField]
    private float cooldownAttack;           // 攻撃クールダウン
    [SerializeField]
    private float moveSpeed;                        // 移動速度

    [SerializeField]
    private GameObject prefabArrow;

    public override void Init()
    {
    }

    private void Update()
    {
        _destPosition *= -_direction;

        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, _destPosition.x, moveSpeed),
            transform.position.y,
            transform.position.z);

        if (IsMoved() && !_playerComponent.IsDead && !_isDead)
        {
            if (_cooldownAttackCounter > 0)
            {
                _cooldownAttackCounter -= Time.deltaTime;
            }
            else
            {
                //Instantiate(prefabArrow, transform);

                _cooldownAttackCounter = cooldownAttack;
            }
        }
    }

    private bool IsMoved()
    {
        return transform.position.x == _destPosition.x;
    }

    // 死亡処理
    public override bool Dead()
    {
        _isDead = true;

        return true;
    }

}
