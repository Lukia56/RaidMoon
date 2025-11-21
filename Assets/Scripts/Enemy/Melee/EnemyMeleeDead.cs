using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeDead : EnemyMeleeProcess
{
    [SerializeField] private EnemyMelee _enemy;

    [SerializeField] private float _destroyCounter;         // 死亡後削除するまでのカウンタ

    // パラメータ
    [SerializeField] private float _deadAnimationTime;      // 死亡後アニメーションの時間

    // 初期化処理
    public override void Init()
    {
        _destroyCounter = 0;
    }

    // 死亡処理の更新
    public override void UpdateProcess()
    {
        DeadDestroyCounter();
    }

    // 死亡処理
    public bool Dead()
    {
        // 無敵ではないなら
        if (_enemy.IsInvincible) return false;

        _enemy.State = EnemyMelee.EState.Dead;
        _destroyCounter = _deadAnimationTime;

        return true;
    }

    // 死亡後削除カウンタの処理
    private void DeadDestroyCounter()
    {
        // 一定時間経過後、自身を削除
        _destroyCounter -= Time.deltaTime;

        if (_destroyCounter < 0)
        {
            _enemy.Release();
        }
    }
}
