using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Enemy
{
    [SerializeField] private GameObject _prefabAttackCollider;

    [SerializeField] private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float _gravity = 0;                // 重力
    [SerializeField] private float _parryExplosionCounter = 0;  // はじかれてから爆発するまでのカウンタ
    [SerializeField] private bool _isParried = false;           // はじかれたかどうか

    [SerializeField] private float _moveTime;                   // 移動時間
    [SerializeField] private float _initForceY;                 // 初速
    [SerializeField] private float _parryForceY;                // はじかれたときの速度
    [SerializeField] private float _parryExplosionTime;         // はじかれてから爆発するまでの時間

    [SerializeField]
    private Vector3 shakePower;
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject impactPrefab;

    public override void Init()
    {
        _velocity = Vector3.zero;
        _gravity = 0;
        _parryExplosionCounter = 0;
        _isParried = false;
    }

    public override void PostInit()
    {
        // _moveTimeの時間をかけて移動するようにする
        _velocity.x = (0 - transform.position.x) / _moveTime;

        _velocity.y = _initForceY;
        _gravity = _initForceY / _moveTime;
    }

    private void Start()
    {
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
    }

    private void Update()
    {
        _velocity.y -= _gravity * 2.0f * Time.deltaTime;

        ParryProcess();

        // 移動
        transform.position += _velocity * Time.deltaTime;

        // 画面外に出たら削除
        if (IsOffscreen()) Release();
    }

    private bool IsOffscreen() { return transform.position.y < -7; }

    // 爆発処理
    private void Explosion()
    {
        //Debug.Log("爆発処理が呼ばれました");

        // カメラを揺らす
        cameraController.Shake(shakePower);

        // 爆発エフェクトを生成
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 攻撃判定を生成
        Instantiate(_prefabAttackCollider, transform.position, Quaternion.identity);
    }

    public override bool Parried()
    {
        if (_isParried) return false;

        _velocity.y = _parryForceY;
        _parryExplosionCounter = _parryExplosionTime;
        _isParried = true;

        Instantiate(impactPrefab, transform.position, Quaternion.identity);

        return true;
    }

    private void ParryProcess()
    {
        if (!_isParried) return;

        _parryExplosionCounter -= Time.deltaTime;

        if (_parryExplosionCounter >= 0) return;
        
        Explosion();
        Release();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 地面にぶつかったら爆発させる
        if (collision.gameObject.tag == "Ground")
        {
            if (_velocity.y > 0) return;

            Explosion();

            // 自身を削除
            Release();
        }
    }
}
