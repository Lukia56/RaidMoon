using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeMove : EnemyMeleeProcess
{
    [SerializeField] EnemyMelee _enemy;
    [SerializeField] EnemyMeleeAttack _attack;

    [SerializeField] private Vector3 _moveSpeed;                                // 移動速度
    public Vector3 MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    [SerializeField] private bool _canMove;                                     // 動けるかどうか
    public bool CanMove { get => _canMove; set => _canMove = value; }

    [SerializeField]
    private float _footstepsCounter;

    // パラメータ
    [SerializeField] private Vector3 _maxRunSpeed;  // 最大走行速度
    [SerializeField] private float _decel;          // 減速度

    [SerializeField]
    private float footstepsDuration;

    [SerializeField]
    private AudioClip seFootsteps;

    // 初期化処理
    public override void Init()
    {
        _canMove = true;
        _footstepsCounter = 0;

        _moveSpeed = Vector3.zero;
    }

    // 移動処理の更新
    public override void UpdateProcess()
    {
        Run();

        if (_attack.CanAttack())
        {
            _enemy.SetState(EnemyMelee.EState.Attack);
        }
    }

    // 移動処理
    public void Move()
    {
        // 移動
        transform.position += _moveSpeed * Time.deltaTime;

        // 減速
        _moveSpeed.x = Mathf.MoveTowards(_moveSpeed.x, 0, _decel * Time.deltaTime);

        // 向きを設定
        transform.localScale = new Vector3(_enemy.Direction, 1, 1);
    }

    // 走り処理
    private void Run()
    {
        // 動けるなら
        if (!_canMove) return;

        _moveSpeed = _maxRunSpeed * _enemy.Direction;

        if (_footstepsCounter > 0)
        {
            _footstepsCounter -= Time.deltaTime;
        }
        else
        {
            _enemy.MyAudioSource.PlayOneShot(seFootsteps);

            _footstepsCounter = footstepsDuration;
        }
    }

    // 強制移動
    public void ForceMove(Vector3 force)
    {
        force.x *= _enemy.Direction;
        _moveSpeed = force;
    }

    // 加速
    public void Accelerate(Vector3 add)
    {
        _moveSpeed += add * Time.deltaTime;
    }
}
