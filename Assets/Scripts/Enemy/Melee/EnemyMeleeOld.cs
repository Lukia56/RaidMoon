using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeOld : PooledObject
{
    [SerializeField] private Vector3 m_MaxRunSpeed;                 // 最大走行速度
    [SerializeField] private float m_Decel;                         // 減速度
    [SerializeField] private Vector3 m_MoveSpeed = Vector3.zero;    // 移動速度
    public Vector3 MoveSpeed { get => m_MoveSpeed; set => m_MoveSpeed = value; }
    [SerializeField] private bool m_CanMove = true;                 // 動けるかどうか
    
    [SerializeField] private int m_Direction = 1;                   // 敵が向いている方向
    public int Direction { get => m_Direction; }

    //[SerializeField] private List<EnemyMeleeProcess> _processList;// 処理のコンポーネント
    [SerializeField] private EnemyMeleeAttack _attack;              // 攻撃処理のコンポーネント
    [SerializeField] private EnemyMeleeMove _move;                  // 移動処理のコンポーネント
    [SerializeField] private EnemyMeleeDead _dead;                  // 死亡処理のコンポーネント

    [SerializeField] private Transform m_PlayerTransform;           // プレイヤーのトランスフォーム
    public Transform PlayerTransform { get => m_PlayerTransform; }
    [SerializeField] private Player m_PlayerComponent;              // プレイヤーのトランスフォーム

    [SerializeField] private float m_ParryKnockbackForce;           // はじかれたときのノックバックの強さ
    [SerializeField] private float m_StunTimeParried;               // はじかれたときのスタンする時間

    [SerializeField] private bool m_IsStun;                         // スタンしているか
    [SerializeField] private float m_StunCounter;                   // スタンのカウンタ

    [SerializeField] private bool m_IsFleeing = false;              // 逃走中かどうか
    [SerializeField] private float m_FleeCounter = 0;               // 攻撃後から逃亡後削除するまでのカウンタ
    [SerializeField] private float m_FleeAnimationTime;             // 逃亡後アニメーションの時間
    [SerializeField] private float m_FleeStartTime;                 // 攻撃後から逃亡開始するまでの時間
    [SerializeField] private bool m_IsJumpFlee = false;             // 逃走中ジャンプをしたか
    [SerializeField] private float m_FleeJumpForce;                 // 逃走時のジャンプ力
    [SerializeField] private float m_Gravity;                       // 重力

    [SerializeField] private bool m_IsDead = false;                 // 死亡したかどうか
    [SerializeField] private bool m_IsInvincible = false;           // 無敵かどうか
    public bool IsInvincible { get => m_IsInvincible; set => m_IsInvincible = value; }
    [SerializeField] private float m_DeadDestroyCounter = 0;        // 死亡後削除するまでのカウンタ
    [SerializeField] private float m_DeadAnimationTime;             // 死亡後アニメーションの時間

    [SerializeField] private State state = State.Idle;

    enum State
    {
        Idle,
        Run,
        Attack,
        PostAttack,
        Parried,
        Stun,
        Flee,
        Dead
    }
    
    public override void Init()
    {
        m_MoveSpeed = Vector3.zero;
        m_CanMove = true;
        m_IsStun = false;
        m_IsFleeing = false;
        m_IsJumpFlee = false;
        m_IsDead = false;
        m_IsInvincible = false;

        _attack.Init();
        _dead.Init();

        //foreach (var process in _processList)
        //{
        //    process.Init();
        //}
    }

    private void Update()
    {
        Action();

        //// 移動
        //transform.position += m_MoveSpeed * Time.deltaTime;

        //m_MoveSpeed.x = Mathf.MoveTowards(m_MoveSpeed.x, 0, m_Decel * Time.deltaTime);

        //// 向きを設定
        //transform.localScale = new Vector3(m_Direction, 1, 1);
    }

    // 行動処理全般
    private void Action()
    {

        //switch (state)
        //{
        //    case State.Idle:
                
        //        break;
            
        //    case State.Run:
        //        _move.UpdateProcess();
        //        break;

        //    case State.Attack:
        //        _attack.UpdateProcess();
        //        break;

        //    case State.PostAttack:

        //        break;

        //    case State.Parried:

        //        break;
        //    case State.Dead:
        //        _dead.UpdateProcess();
        //        break;
        //}





        

        // スタンしているなら
        //if (m_IsStun)
        //{
        //    StunCounter();
        //}

        

        // プレイヤーが死亡しているなら
        if (m_PlayerComponent.IsDead) return;

        // 走り処理
        //Run();

        // 攻撃処理
        _attack.Attack();

        // 逃走開始カウンタの処理
        //FleeStartCounter();
    }

    // 走り処理
    //private void Run()
    //{
    //    // 動けるなら
    //    if (!m_CanMove) return;

    //    // 攻撃していないなら
    //    if (_attack.IsAttacked) return;

    //    m_MoveSpeed = m_MaxRunSpeed * m_Direction;
    //}

    // 逃走開始カウンタの処理
    //private void FleeStartCounter()
    //{
    //    // 攻撃前なら
    //    if (!_attack.IsAttacked)
    //    {
    //        m_FleeCounter = 0;

    //        return;
    //    }

    //    // 攻撃後かつ、
    //    // 一定時間経過後、逃走開始
    //    m_FleeCounter += Time.deltaTime;

    //    if (m_FleeCounter > m_FleeStartTime)
    //    {
    //        m_IsFleeing = true;
    //    }
    //}

    // 逃走後削除カウンタの処理
    //private void FleeDestroyCounter()
    //{
    //    // 一定時間経過後、自身を削除
    //    m_FleeCounter += Time.deltaTime;

    //    if (!m_IsJumpFlee)
    //    {
    //        m_MoveSpeed.y = m_FleeJumpForce;

    //        m_IsJumpFlee = true;
    //    }

    //    m_MoveSpeed.y -= m_Gravity * Time.deltaTime;

    //    m_IsInvincible = true;

    //    if (m_FleeCounter > m_FleeStartTime + m_FleeAnimationTime)
    //    {
    //        Release();
    //    }
    //}

    //// 死亡後削除カウンタの処理
    //private void DeadDestroyCounter()
    //{
    //    // 一定時間経過後、自身を削除
    //    m_DeadDestroyCounter -= Time.deltaTime;

    //    if (m_DeadDestroyCounter < 0)
    //    {
    //        Release();
    //    }
    //}

    // 攻撃がはじかれた処理
    //public void Parried()
    //{
    //    Knockback(m_ParryKnockbackForce);
    //}

    //// ノックバック
    //private void Knockback(float force)
    //{
    //    m_MoveSpeed = Vector3.right * force * -m_Direction;
    //    m_CanMove = false;
    //    m_IsStun = true;
    //}

    //// スタンカウンタの処理
    //private void StunCounter()
    //{
    //    // スタンしていないなら
    //    if (!m_IsStun)
    //    {
    //        m_StunCounter = 0;
    //    }

    //    // 一定時間経過後、逃走
    //    m_StunCounter += Time.deltaTime;

    //    if (m_StunCounter > m_StunTimeParried)
    //    {
    //        m_IsFleeing = true;
    //    }
    //}

    // 死亡処理
    public void Dead()
    {
        //Debug.Log("敵の死亡処理");

        _dead.Dead();
    }

    //// ダッシュ
    //public void Dash(Vector3 force)
    //{
    //    m_MoveSpeed = force * m_Direction;
    //}

    // 向いている方向を設定する
    public void SetDirection(int direction) { m_Direction = direction; }

    // プレイヤーのトランスフォームのセッター
    public void SetPlayerTransform(Transform player) { m_PlayerTransform = player; }

    // プレイヤーコンポーネントのセッター
    public void SetPlayerComponent(Player player) { m_PlayerComponent = player; }
}
