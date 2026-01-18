using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeParried : EnemyMeleeProcess
{
    [SerializeField] private EnemyMelee _enemy;
    [SerializeField] private EnemyMeleeMove _move;

    [SerializeField] private bool _isKnockbacked;               // ノックバックしたか
    [SerializeField] private float _stunCounter;                // スタンのカウンタ

    // パラメータ
    [SerializeField]
    private float _parryKnockbackForce;     // はじかれたときのノックバックの強さ
    [SerializeField]
    private float _stunTimeParried;         // はじかれたときのスタンする時間
    [SerializeField]
    private Vector3 offsetImpactPos;
    [SerializeField]
    private GameObject impactPrefab;

    // 初期化処理
    public override void Init()
    {
        _isKnockbacked = false;

        _stunCounter = 0;
    }

    // 死亡処理の更新
    public override void UpdateProcess()
    {
        if (!_isKnockbacked)
        {
            Parried();
        }
        else
        {
            StunCounter();
        }
    }

    // 攻撃がはじかれた処理
    public void Parried()
    {
        Knockback(_parryKnockbackForce);

        _isKnockbacked = true;

        Vector3 rot = transform.localEulerAngles;
        rot.z = 90 * -_enemy.Direction;
        Instantiate(impactPrefab, transform.position + offsetImpactPos, Quaternion.Euler(rot));
    }

    // ノックバック
    private void Knockback(float force)
    {
        //_move.MoveSpeed = Vector3.right * force * -_enemy.Direction;
        _move.ForceMove(Vector3.left * force);
        _move.CanMove = false;
    }

    // スタンカウンタの処理
    private void StunCounter()
    {
        // 一定時間経過後、逃走
        _stunCounter += Time.deltaTime;

        if (_stunCounter > _stunTimeParried)
        {
            _enemy.SetState(EnemyMelee.EState.Flee);
        }
    }
}
