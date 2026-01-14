using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeFlee : EnemyMeleeProcess
{
    [SerializeField] private EnemyMelee _enemy;
    public EnemyMelee Enemy { set => _enemy = value; }
    [SerializeField] private EnemyMeleeMove _move;

    [SerializeField]
    private float _fleeCounter;         // 攻撃後から逃亡後削除するまでのカウンタ
    [SerializeField]
    private bool _isJumpFlee;           // 逃走中ジャンプをしたか
    [SerializeField]
    private bool _isCreateParticle;     // パーティクルを生成したか

    // パラメータ
    [SerializeField]
    private float _fleeAnimationTime;   // 逃亡後アニメーションの時間
    [SerializeField]
    private float _fleeStartTime;       // 攻撃後から逃亡開始するまでの時間
    [SerializeField]
    private float _fleeJumpForce;       // 逃走時のジャンプ力
    [SerializeField]
    private Vector3 _gravity;           // 逃走時の重力
    [SerializeField]
    private int propSortingLayer;       // 背景の表示順番
    [SerializeField]
    private SpriteRenderer myRenderer;
    [SerializeField]
    private GameObject grassParticle;   // 飛び散る草のプレハブ

    // 初期化処理
    public override void Init()
    {
        _isJumpFlee = false;
        _isCreateParticle = false;

        _fleeCounter = 0;

        myRenderer.sortingOrder = 0;
    }

    // 逃走処理の更新
    public override void UpdateProcess()
    {
        _enemy.IsInvincible = true;

        // ジャンプ
        Jump();

        // 重力
        _move.Accelerate(-_gravity);

        // 落下しているなら
        if (IsFalling())
        {
            // 背景より後ろに移動させる
            myRenderer.sortingOrder = propSortingLayer - 1;
        }

        // プレイヤーより下にいったらパーティクルを生成する
        if (CanCreateParticle())
        {
            Instantiate(grassParticle, transform.position, Quaternion.identity);

            _isCreateParticle = true;
        }

        FleeCounter();
    }

    // ジャンプ
    private void Jump()
    {
        // 逃走開始後一度だけジャンプさせる
        if (_isJumpFlee) return;

        _move.ForceMove(new Vector3(0, _fleeJumpForce, 0));

        _isJumpFlee = true;
    }

    // 逃走カウンタの処理
    private void FleeCounter()
    {
        // 一定時間経過後、自身を削除
        _fleeCounter += Time.deltaTime;

        //m_MoveSpeed.y -= m_Gravity * Time.deltaTime;

        if (_fleeCounter > _fleeAnimationTime)
        {
            _enemy.Release();
        }
    }

    // 落下中かどうか
    private bool IsFalling() { return _move.MoveSpeed.y < 0; }

    // パーティクルを生成できるかどうか
    private bool CanCreateParticle()
    {
        // プレイヤーより下にいないならfalse
        if (!(transform.position.y < _enemy.PlayerTransform.position.y)) return false;
        // すでにパーティクルを生成したならfalse
        if (_isCreateParticle) return false;
        // 落下していないならfalse
        if (!IsFalling()) return false;

        return true;
    }
}
