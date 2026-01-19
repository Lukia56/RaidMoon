using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField]
    private EState _state;
    public EState State { get { return _state; } set { if (_state != EState.Dead) _state = value;} }
    [SerializeField]
    private EnemyMeleeMove _move;          // 移動処理のコンポーネント
    [SerializeField]
    private EnemyMeleeAttack _attack;      // 攻撃処理のコンポーネント
    [SerializeField]
    private EnemyMeleeParried _parried;    // はじかれ処理のコンポーネント
    [SerializeField]
    private EnemyMeleeFlee _flee;          // 逃走処理のコンポーネント
    [SerializeField]
    private EnemyMeleeDead _dead;          // 死亡処理のコンポーネント
    [SerializeReference]
    private List<EnemyMeleeProcess> _processList;
    [SerializeField]
    private Animator animator;

    [SerializeField] private bool _isInvincible;            // 無敵かどうか
    public bool IsInvincible { get => _isInvincible; set => _isInvincible = value; }
    [SerializeField]
    private bool m_isAnimationUpdated;                      // 現在のフレームでアニメーションが更新されたかどうか
    [SerializeField] GameObject _arrow;
    public GameObject Arrow { get => _arrow; }

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
        SetState(EState.Run);
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

    private void FixedUpdate()
    {
        m_isAnimationUpdated = false;
    }

    private void Update()
    {
        Action();

        if (!m_isAnimationUpdated)
        {
            //SetState(EState.Idle);
        }
    }

    // 行動処理全般
    private void Action()
    {
        // プレイヤーが死亡したら止まる
        if (_playerComponent.IsDead)
        {
            if (_state == EState.Run)
                SetState(EState.Idle);
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
    public override bool Dead()
    {
        return ((EnemyMeleeDead)_processList[(int)EState.Dead]).Dead();
    }

    // はじかれ処理
    public override bool Parried()
    {
        if (_state == EState.Dead) return false;

        SetState(EState.Parried);

        return true;
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

    public void SetState(EState state)
    {
        _state = state;
        animator.SetInteger("State", (int)_state);
        m_isAnimationUpdated = true;
    }
}
