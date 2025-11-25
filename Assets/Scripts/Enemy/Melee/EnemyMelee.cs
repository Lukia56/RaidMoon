using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField] private EState _state = EState.Run;
    [SerializeField] private EnemyMeleeMove _move;          // 移動処理のコンポーネント
    [SerializeField] private EnemyMeleeAttack _attack;      // 攻撃処理のコンポーネント
    [SerializeField] private EnemyMeleeParried _parried;    // はじかれ処理のコンポーネント
    [SerializeField] private EnemyMeleeFlee _flee;          // 逃走処理のコンポーネント
    [SerializeField] private EnemyMeleeDead _dead;          // 死亡処理のコンポーネント
    [SerializeReference]
    private List<EnemyMeleeProcess> _processList;

    [SerializeField] private int _direction;            // 敵が向いている方向
    public int Direction { get => _direction; set => _direction = value; }
    [SerializeField] private Transform _playerTransform;    // プレイヤーのトランスフォーム
    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
    [SerializeField] private Player _playerComponent;       // プレイヤーのトランスフォーム
    public Player PlayerComponent { get => _playerComponent; set => _playerComponent = value; }
    [SerializeField] private bool _isInvincible;             // 無敵かどうか
    public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
    [SerializeField] GameObject _arrow;
    public GameObject Arrow { get => _arrow; }

    public EState State
    {
        get { return _state; }
        set
        {
            if (_state != EState.Dead) _state = value;
        }
    }

    public enum EState
    {
        Idle,
        Run,
        Attack,
        Parried,
        Flee,
        Dead
    }

    public override void Init()
    {
        _state = EState.Run;
        _isInvincible = false;
        _direction = 1;

        //_move.Init();
        //_attack.Init();
        //_parried.Init();
        //_flee.Init();
        //_dead.Init();

        foreach (var process in _processList)
        {
            process.Init();
        }
    }

    private void Update()
    {
        Action();
    }

    // 行動処理全般
    private void Action()
    {
        // プレイヤーが死亡したら止まる
        if (_playerComponent.IsDead)
        {
            if (_state == EState.Run
            || _state == EState.Attack)
                _state = EState.Idle;
        }

        switch (_state)
        {
            case EState.Idle:

                break;

            case EState.Run:
                _processList[(int)EState.Run].UpdateProcess();
                break;

            case EState.Attack:
                _processList[(int)EState.Attack].UpdateProcess();
                break;

            case EState.Parried:
                _processList[(int)EState.Parried].UpdateProcess();
                break;

            case EState.Flee:
                _processList[(int)EState.Flee].UpdateProcess();
                break;

            case EState.Dead:
                _processList[(int)EState.Dead].UpdateProcess();
                break;
        }

        ((EnemyMeleeMove)_processList[(int)EState.Run]).Move();
    }

    // 死亡処理
    public bool Dead()
    {
        return ((EnemyMeleeDead)_processList[(int)EState.Dead]).Dead();
    }

    // はじかれ処理
    public override void Parried()
    {
        State = EState.Parried;
    }

    // 2つの座標が近いかどうかを調べる
    public bool IsNearPosition(Vector3 position1, Vector3 position2, float range)
    {
        // 距離を計算
        float distanceSquare = Mathf.Pow(position1.x - position2.x, 2.0f) + Mathf.Pow(position1.y - position2.y, 2.0f);

        // 範囲を計算
        float rangeSquare = Mathf.Pow(range, 2.0f);

        return distanceSquare <= rangeSquare;
    }
}
