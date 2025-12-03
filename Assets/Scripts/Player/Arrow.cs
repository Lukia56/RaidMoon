using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PooledObject
{
    // 速度
    [SerializeField] private Vector3 _speed = Vector3.zero;
    // 方向
    [SerializeField] private int _direction = 0;
    public int Direction { get => _direction; set => _direction = value; }
    // チャージ率
    [SerializeField] private float _chargeRate = 0;
    public float ChargeRate { get => _chargeRate; set => _chargeRate = value; }
    // 攻撃判定のオブジェクト
    [SerializeField] private GameObject _collider;

    // パラメータ
    [SerializeField] private float _power;              // 発射時の力
    [SerializeField] private float _decel;              // 減速度
    [SerializeField] private float _gravity;            // 重力度
    [SerializeField] private float _minPowerRate;       // 最小の初速の割合
    [SerializeField] private float _startFallXSpeed;    // 落下し始めるX速度

    [SerializeField] private GameObject _prefabColliderAttack;

    public override void Init()
    {
        _speed = Vector3.zero;
    }

    public void PostInit()
    {
        // 初速を設定
        _speed.x = CalculateInitSpeedX();

        // 攻撃判定の初期化
        _collider.GetComponent<ColliderBowAttack>().IsCharged = (_chargeRate >= 1.0f);  // チャージ済みがどうかを攻撃判定に渡す
        _collider.GetComponent<ColliderBowAttack>().IsDead = false;
    }

    private void Awake()
    {
        // 自身の攻撃判定を生成
        _collider = Instantiate(_prefabColliderAttack, transform);
    }

    private void Update()
    {
        Movement();

        EndProcess();
    }

    private void Movement()
    {
        // 減速
        _speed.x = Mathf.MoveTowards(_speed.x, 0, _decel * Time.deltaTime);

        // 落下
        Fall();

        // 移動
        transform.position += _speed * Time.deltaTime;
    }

    private void Fall()
    {
        // 勢いが落ちたら落下させる
        if (!IsStartingFall()) return;

        // 落下
        _speed.y -= _gravity * Time.deltaTime;
    }

    private float CalculateInitSpeedX()
    {
        float rate = Mathf.Max(_chargeRate, _minPowerRate);

        return _power * rate * _direction;
    }

    private bool IsStartingFall()
    {
        return _speed.x < _startFallXSpeed;
    }

    private void EndProcess()
    {
        // 攻撃判定が消えたら自身を削除
        if (!_collider.GetComponent<ColliderBowAttack>().IsDead) return;

        Release();
    }
}
