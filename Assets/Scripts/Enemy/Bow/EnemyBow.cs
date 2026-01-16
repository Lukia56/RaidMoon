using UnityEngine;

public class EnemyBow : Enemy
{
    enum State
    {
        Idle,
        Walk,
        Fire,
        Dead
    }

    [SerializeField]
    private State m_state;

    [SerializeField]
    private float m_delayAttackCounter;     // 攻撃を遅らせるカウンタ
    [SerializeField]
    private float _cooldownAttackCounter;   // 攻撃クールダウンのカウンタ
    [SerializeField]
    private bool _isDead;
    [SerializeField]
    private float _destroyCounter;          // 死亡後削除するまでのカウンタ

    // パラメータ
    [SerializeField]
    Vector3 _destPosition;
    [SerializeField]
    private float delayAttackTime;          // 攻撃を遅らせる時間
    [SerializeField]
    private float cooldownAttack;           // 攻撃クールダウン
    [SerializeField]
    private float moveSpeed;                // 移動速度
    [SerializeField]
    private float deadAnimationTime;        // 死亡後アニメーションの時間
    [SerializeField]
    private Vector3 offsetArrowPos;         // 発射時の弾の座標のずらす量

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject prefabArrow;

    public override void Init()
    {
        SetState(State.Idle);
        m_delayAttackCounter = 0;
        _cooldownAttackCounter = 0;
        _isDead = false;
        _destroyCounter = 0;
        spriteRenderer.enabled = true;
    }

    public override void PostInit()
    {
    }

    private void FixedUpdate()
    {
        if (m_state != State.Dead) SetState(State.Idle);
    }

    private void Update()
    {
        Move();

        CountAttackCooldown();

        if (CanAttack())
        {
            Attack();
        }

        DeadDestroyCounter();

        transform.localScale = new Vector3(Direction, 1, 1);
    }

    // 移動処理
    private void Move()
    {
        // 移動し終わっているなら移動しない
        if (IsMoved()) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(transform.position.x, _destPosition.x * -_direction, moveSpeed);
        transform.position = pos;

        SetState(State.Walk);
    }

    // 攻撃処理
    private void Attack()
    {
        if (m_delayAttackCounter == 0) SetState(State.Fire);

        if (CountDelayAttack())
        {
            Vector3 offset = offsetArrowPos;
            offset.x *= _direction;

            // 弾幕を発射する
            Instantiate(prefabArrow, transform.position + offset, Quaternion.identity);

            m_delayAttackCounter = 0;

            // クールダウンを設定する
            _cooldownAttackCounter = cooldownAttack;
        }
    }

    // 移動し終わったかどうか
    private bool IsMoved() { return transform.position.x == _destPosition.x * -_direction; }

    // 攻撃可能かどうか
    private bool CanAttack()
    {
        // 移動し終わった
        if (!IsMoved()) return false;

        // クールダウンが終わった
        if (_cooldownAttackCounter > 0) return false;

        // プレイヤーが生きている
        if (_playerComponent.IsDead) return false;

        // 自身が生きている
        if (_isDead) return false;

        return true;
    }

    private bool CountDelayAttack()
    {
        if (m_delayAttackCounter < delayAttackTime)
        {
            m_delayAttackCounter += Time.deltaTime;

            return false;
        }
        else
        {
            return true;
        }
    }

    // 攻撃クールダウンのカウント
    private void CountAttackCooldown()
    {
        if (!IsMoved()) return;
        
        _cooldownAttackCounter -= Time.deltaTime;
    }

    // 死亡後削除カウンタの処理
    private void DeadDestroyCounter()
    {
        if (!_isDead) return;

        // 一定時間経過後、自身を削除
        _destroyCounter -= Time.deltaTime;

        if (_destroyCounter < 0)
        {
            Release();
        }

        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    private void SetState(State state)
    {
        m_state = state;
        animator.SetInteger("State", (int)m_state);
    }

    // 死亡処理
    public override bool Dead()
    {
        _isDead = true;
        _destroyCounter = deadAnimationTime;

        KillNumbers.AddNumber();

        SetState(State.Dead);

        return true;
    }
}
