using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _direction = 1;                    // プレイヤーが向いている方向
    [SerializeField] private bool _isDead = false;                  // 死亡したかどうか
    public bool IsDead { get { return _isDead; } }
    [SerializeField] private bool _isInvincible = false;            // 無敵かどうか

    [SerializeField] private float _actionCooldownCounter = 0;      // 行動クールダウンのカウンタ

    [SerializeField] private int _numArrows;                        // 矢の数
    [SerializeField] private float _bowChargeCounter = 0;           // 弓のチャージカウンタ
    [SerializeField] private bool _isBowStartCharging = false;      // 弓がチャージ中かどうか
    [SerializeField] private bool _isBowCharged = false;            // 弓がチャージできたか

    [SerializeField] private float _dodgeCounter = 0;

    // パラメータ
    [SerializeField] private float _cooldownKatanaAttack;          // 刀攻撃のクールダウン時間
    [SerializeField] private float _cooldownBowAttack;             // 弓攻撃のクールダウン時間
    [SerializeField] private float _cooldownDodge;                 // 回避アクションのクールダウン時間
    [SerializeField] private float _cooldownParry;                 // はじきアクションのクールダウン時間

    [SerializeField] private float _bowChargeTime;                  // 弓のチャージ完了までの時間
    [SerializeField] private float _dodgeDuration;                  // 回避の無敵持続時間
    [SerializeField] private Vector3 _offsetAttackCollider;        // 刀攻撃判定のオフセット
    [SerializeField] private Vector3 _offsetParryCollider;         // はじき判定のオフセット

    [SerializeField] private GameObject _prefabAttackCollider;     // 刀攻撃判定のプレハブ
    [SerializeField] private GameObject _prefabParryCollider;      // はじき判定のプレハブ
    [SerializeField] private GameObject _prefabArrow;               // 矢のプレハブ
    [SerializeField] private ObjectPool _arrowObjectPool;           // 矢のオブジェクトプール
    [SerializeField] private SpriteRenderer _renderer;

    private void Update()
    {
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

        transform.localScale = new Vector3(_direction, 1, 1);
    }

    // アクションの入力分岐
    private void ActionInput()
    {
        // 刀攻撃の処理
        if (Input.GetKeyDown(KeyCode.Z))
        {
            KatanaAttack();
        }

        // 弓攻撃の処理
        if (Input.GetKey(KeyCode.LeftShift) && _numArrows > 0)
        {
            BowAttack();
        }
        if (_isBowStartCharging && (Input.GetKeyUp(KeyCode.LeftShift) || _isBowCharged))
        {
            CreateArrow();

            _isBowStartCharging = false;
            _isBowCharged = false;
            _bowChargeCounter = 0;
            _numArrows--;

            // 行動クールダウンを設定する
            SetActionCooldown(_cooldownBowAttack);
        }

        // 回避アクションの処理
        if (Input.GetKeyDown(KeyCode.X))
        {
            Dodge();
        }

        // はじきアクションの処理
        if (Input.GetKeyDown(KeyCode.C))
        {
            Parry();
        }
    }
    
    // 刀攻撃の処理
    private void KatanaAttack()
    {
        //Debug.Log("刀の攻撃処理が呼ばれました");

        // 攻撃判定を生成する
        CreateCollider(_prefabAttackCollider, _offsetAttackCollider);

        // 行動クールダウンを設定する
        SetActionCooldown(_cooldownKatanaAttack);
    }

    // 弓攻撃の処理
    private void BowAttack()
    {
        //Debug.Log("弓のチャージ開始");

        _isBowStartCharging = true;
        _bowChargeCounter += Time.deltaTime;

        if (_bowChargeCounter > _bowChargeTime)
        {
            _isBowCharged = true;
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
        arrow.transform.position = transform.position;
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

        // はじき判定を生成する
        CreateCollider(_prefabParryCollider, _offsetParryCollider);

        // 行動クールダウンを設定する
        SetActionCooldown(_cooldownParry);
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
        Instantiate(_prefab, pos, Quaternion.identity);
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

        if (Input.GetKeyDown(KeyCode.RightArrow)) _direction = 1;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) _direction = -1;
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

        Debug.Log("死亡しました");
        _isDead = true;
    }

    // 矢の補充
    public void AddArrow(int add)
    {
        _numArrows += add;
    }
}
