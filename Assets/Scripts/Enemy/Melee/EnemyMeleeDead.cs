using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeDead : EnemyMeleeProcess
{
    [SerializeField] private EnemyMelee _enemy;

    [SerializeField] private float _destroyCounter;         // 死亡後削除するまでのカウンタ

    [SerializeField]
    private bool _isBloodCreated;

    // パラメータ
    [SerializeField]
    private float _deadAnimationTime;   // 死亡後アニメーションの時間
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private GameObject bloodFx;

    // 初期化処理
    public override void Init()
    {
        _spriteRenderer.enabled = true;
        _destroyCounter = 0;
        _isBloodCreated = false;
    }

    // 死亡処理の更新
    public override void UpdateProcess()
    {
        if (!_isBloodCreated)
        {
            GameObject blood = Instantiate(bloodFx, transform.position, Quaternion.identity);
            blood.transform.localScale = new Vector3(-_enemy.Direction, 1, 1);

            _isBloodCreated = true;
        }

        _spriteRenderer.enabled = !_spriteRenderer.enabled;

        DeadDestroyCounter();
    }

    // 死亡処理
    public bool Dead()
    {
        // 無敵ではないなら
        if (_enemy.IsInvincible) return false;

        // まだ死亡していないなら
        if (_enemy.State == EnemyMelee.EState.Dead) return false;

        _enemy.SetState(EnemyMelee.EState.Dead);
        _destroyCounter = _deadAnimationTime;

        _enemy.KillNumbers.AddNumber();

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

            GameObject arrow = Instantiate(_enemy.Arrow, _enemy.transform.position, Quaternion.identity);
            arrow.GetComponent<DroppedArrow>().PlayerTransform = _enemy.PlayerTransform; 
            arrow.GetComponent<DroppedArrow>().PlayerComponent = _enemy.PlayerComponent;
        }
    }
}
