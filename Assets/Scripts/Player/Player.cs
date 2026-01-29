using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    enum State
    {
        Idle,
        Attack,
        BowReady,
        BowFire,
        Parry,
        Dodge,
        Dead
    }

    [SerializeField]
    State m_state;
    [SerializeField]
    private int _direction = 1;                 // プレイヤーが向いている方向
    [SerializeField]
    private bool _isDead = false;               // 死亡したかどうか
    public bool IsDead { get { return _isDead; } }
    [SerializeField]
    private bool _isInvincible = false;         // 無敵かどうか

    [SerializeField]
    private float _actionCooldownCounter = 0;   // 行動クールダウンのカウンタ

    [SerializeField]
    private float _delayKatanaAttackCounter;    // 刀攻撃の攻撃判定が出るまでの遅延時間のカウンタ
    [SerializeField]
    private float _delayParryCounter;           // はじきのはじき判定が出るまでの遅延時間のカウンタ

    [SerializeField]
    private bool _isAttacked;
    [SerializeField]
    private bool _isParried;

    [SerializeField]
    private int _numArrows;                     // 矢の数
    public int NumArrows { get { return _numArrows; }}
    [SerializeField]
    private float _bowChargeCounter = 0;        // 弓のチャージカウンタ
    public float BowChargeCounter { get { return _bowChargeCounter; } }
    [SerializeField]
    private bool _isBowStartCharging = false;   // 弓がチャージ中かどうか
    [SerializeField]
    private bool _isBowCharged = false;         // 弓がチャージできたか

    [SerializeField]
    private float _dodgeCounter = 0;            // 回避の無敵時間のカウンタ

    [SerializeField]
    private bool m_isAnimationUpdated;          // 現在のフレームでアニメーションが更新されたかどうか

    private InputAction _moveAction;
    private InputAction _katanaAction;
    private InputAction _bowAction;
    private InputAction _dodgeAction;
    private InputAction _parryAction;

    [Header("パラメータ")]

    [SerializeField]
    private float _cooldownKatanaAttack;        // 刀攻撃のクールダウン時間
    [SerializeField]
    private float _cooldownBowAttack;           // 弓攻撃のクールダウン時間
    [SerializeField]
    private float _cooldownDodge;               // 回避アクションのクールダウン時間
    [SerializeField]
    private float _cooldownParry;               // はじきアクションのクールダウン時間

    [SerializeField]
    private float delayKatanaAttack;            // 刀攻撃の攻撃判定が出るまでの遅延時間
    [SerializeField]
    private float delayParry;                   // はじきのはじき判定が出るまでの遅延時間

    [SerializeField]
    private Vector3 deadCameraShake;            // 死亡時のカメラの揺れの強さ

    [SerializeField]
    private float _bowChargeTime;               // 弓のチャージ完了までの時間
    public float BowChargeTime { get { return _bowChargeTime; } }
    [SerializeField]
    private float _dodgeDuration;               // 回避の無敵持続時間
    [SerializeField]
    private Vector3 _offsetAttackCollider;      // 刀攻撃判定のオフセット
    [SerializeField]
    private Vector3 _offsetParryCollider;       // はじき判定のオフセット
    [SerializeField]
    private Vector3 offsetArrow;

    [SerializeField]
    private GameObject _prefabAttackCollider;   // 刀攻撃判定のプレハブ
    [SerializeField]
    private GameObject _prefabParryCollider;    // はじき判定のプレハブ
    [SerializeField]
    private ObjectPool _arrowObjectPool;        // 矢のオブジェクトプール
    [SerializeField]
    private SpriteRenderer _renderer;           // 自身のレンダラー
    [SerializeField]
    private GameObject bloodFx;                 // 血しぶきのプレハブ
    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private AudioSource audioSource;
    // 風切り音
    [SerializeField]
    private AudioClip seStrikeout;
    // 回避SE
    [SerializeField]
    private AudioClip seDodge;
    // 弓を引き絞るSE
    [SerializeField]
    private AudioClip seDrawBow;
    // 弓をチャージするSE
    [SerializeField]
    private AudioClip seChargeBow;
    // 弓のチャージ完了SE
    [SerializeField]
    private AudioClip seDoneCharge;
    // 弓の発射SE
    [SerializeField]
    private AudioClip seFireBow;
    // チャージした弓の発射SE
    [SerializeField]
    private AudioClip seFireChargedBow;
    // 倒れたときのSE
    [SerializeField]
    private AudioClip seDefeated;

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _katanaAction = InputSystem.actions.FindAction("KatanaAttack");
        _bowAction = InputSystem.actions.FindAction("BowAttack");
        _dodgeAction = InputSystem.actions.FindAction("Dodge");
        _parryAction = InputSystem.actions.FindAction("Parry");

        Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        m_isAnimationUpdated = false;
    }

    private void Update()
    {
        m_state = State.Idle;

        // 向いている方向を設定する
        SetDirectionInput();

        DodgeCount();

        // 行動が可能ではないなら
        if (!CanAction())
        {
            // 行動クールダウンをカウントダウン
            _actionCooldownCounter -= Time.deltaTime;
        }
        // 行動が可能なら
        else
        {
            // アクションの入力分岐
            ActionInput();
        }

        if (_delayKatanaAttackCounter > 0)
        {
            _delayKatanaAttackCounter -= Time.deltaTime;
        }
        if (_delayKatanaAttackCounter <= 0 && _isAttacked)
        {
            // 攻撃判定を生成する
            CreateCollider(_prefabAttackCollider, _offsetAttackCollider);

            _isAttacked = false;
        }

        if (_delayParryCounter > 0)
        {
            _delayParryCounter -= Time.deltaTime;
        }
        if (_delayParryCounter <= 0 && _isParried)
        {
            // はじき判定を生成する
            CreateCollider(_prefabParryCollider, _offsetParryCollider);

            _isParried = false;
        }

        if (!m_isAnimationUpdated)
        {
            SetState(State.Idle);
        }

        transform.localScale = new Vector3(_direction, 1, 1);
    }

    // アクションの入力分岐
    private void ActionInput()
    {
        // 刀攻撃の処理
        if (_katanaAction.WasPressedThisFrame())
        {
            KatanaAttack();
        }

        // 弓攻撃の処理
        if (_bowAction.WasPressedThisFrame() && _numArrows > 0)
        {
            audioSource.PlayOneShot(seDrawBow);
            audioSource.PlayOneShot(seChargeBow);
        }
        if (_bowAction.IsPressed() && _numArrows > 0)
        {
            BowAttack();
        }
        if (_isBowStartCharging && _bowAction.WasReleasedThisFrame())
        {
            CreateArrow();

            audioSource.PlayOneShot(seFireBow);
            if (_isBowCharged)
                audioSource.PlayOneShot(seFireChargedBow);
            
            _isBowStartCharging = false;
            _isBowCharged = false;
            _bowChargeCounter = 0;
            _numArrows--;

            SetState(State.BowFire);

            // 行動クールダウンを設定する
            SetActionCooldown(_cooldownBowAttack);
        }

        // 回避アクションの処理
        if (_dodgeAction.WasPressedThisFrame())
        {
            Dodge();
        }

        // はじきアクションの処理
        if (_parryAction.WasPressedThisFrame())
        {
            Parry();
        }
    }
    
    // 刀攻撃の処理
    private void KatanaAttack()
    {
        //Debug.Log("刀の攻撃処理が呼ばれました");

        _delayKatanaAttackCounter = delayKatanaAttack;
        _isAttacked = true;

        // 行動クールダウンを設定する
        SetActionCooldown(_cooldownKatanaAttack);

        // 風切り音を再生
        audioSource.PlayOneShot(seStrikeout);

        SetState(State.Attack);
    }

    // 弓攻撃の処理
    private void BowAttack()
    {
        //Debug.Log("弓のチャージ開始");

        _isBowStartCharging = true;
        _bowChargeCounter += Time.deltaTime;

        SetState(State.BowReady);

        if (_bowChargeCounter > _bowChargeTime && !_isBowCharged)
        {
            _isBowCharged = true;

            audioSource.PlayOneShot(seDoneCharge);
        }
    }

    // 矢の作成
    private void CreateArrow()
    {
        //GameObject arrow = Instantiate(_prefabArrow, transform.position, Quaternion.identity);
        //Arrow arrowComp = arrow.GetComponent<Arrow>();
        //arrowComp.Direction = m_Direction;
        //arrowComp.ChargeRate = _bowChargeCounter / _bowChargeTime;
        GameObject arrow = _arrowObjectPool.GetInstance().gameObject;
        arrow.transform.position = transform.position + offsetArrow;
        Arrow arrowComp = arrow.GetComponent<Arrow>();
        arrowComp.Direction = _direction;
        arrowComp.ChargeRate = _bowChargeCounter / _bowChargeTime;
        arrowComp.PostInit();
    }

    // 回避アクションの処理
    private void Dodge()
    {
        //Debug.Log("回避の処理が呼ばれました");

        _dodgeCounter = _dodgeDuration;

        audioSource.PlayOneShot(seDodge);

        // 行動クールダウンを設定する
        SetActionCooldown(_cooldownDodge);
    }

    // 回避アクションのカウント処理
    private void DodgeCount()
    {
        if (_dodgeCounter > 0)
        {
            _isInvincible = true;
            _renderer.enabled = !_renderer.enabled;

            _dodgeCounter -= Time.deltaTime;

            SetState(State.Dodge);
        }
        else
        {
            _isInvincible= false;
            _renderer.enabled = true;
        }
    }

    // はじきアクションの処理
    private void Parry()
    {
        //Debug.Log("はじきアクションが呼ばれた");

        _delayParryCounter = delayParry;
        _isParried = true;

        // 行動クールダウンを設定する
        SetActionCooldown(_cooldownParry);

        audioSource.PlayOneShot(seStrikeout);

        SetState(State.Parry);
    }

    // 判定を生成する
    private void CreateCollider(GameObject _prefab, Vector3 _offset)
    {
        // オフセットの方向を指定する
        Vector3 offset = _offset;
        offset.x *= _direction;
        // 判定の座標を設定
        Vector3 pos = transform.position + offset;
        // 生成
        Instantiate(_prefab, pos, Quaternion.identity, transform);
    }
    
    // アクションができるかどうか
    private bool CanAction()
    {
        // クールダウンが発生していない
        if (_actionCooldownCounter > 0) return false;

        // 死亡していない
        if (_isDead) return false;

        return true;
    }

    // 入力で向いている方向を設定する
    private void SetDirectionInput()
    {
        if (_isDead) return;

        var moveValue = _moveAction.ReadValue<Vector2>();

        if (moveValue.x == 0.0f) return;

        Debug.Log(moveValue);
        _direction = Mathf.CeilToInt(Mathf.Abs(moveValue.x)) * (int)Mathf.Sign(moveValue.x);
    }

    // 行動クールダウンを設定する
    private void SetActionCooldown(float time)
    {
        // 時間がマイナスなら0にする
        if (time < 0) time = 0;
        
        _actionCooldownCounter = time;
    }

    // 死亡処理
    public void Dead()
    {
        if (_isInvincible) return;

        //Debug.Log("死亡しました");

        _isDead = true;

        Instantiate(bloodFx, transform.position, Quaternion.identity);

        cameraController.Shake(deadCameraShake);

        audioSource.PlayOneShot(seDefeated);

        SetState(State.Dead);
    }

    // 矢の補充
    public void AddArrow(int add)
    {
        _numArrows += add;
    }

    // 状態を設定する
    private void SetState(State state)
    {
        m_state = state;
        animator.SetInteger("State", (int)m_state);
        m_isAnimationUpdated = true;
    }
}
