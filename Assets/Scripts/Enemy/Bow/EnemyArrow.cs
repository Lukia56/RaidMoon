using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyArrow : Enemy
{
    [SerializeField]
    private Vector3 _velocity = Vector3.zero;
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private bool _isStartRotate;
    [SerializeField]
    private GameObject _collider;

    // パラメータ
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float initForceY;
    [SerializeField]
    private float rotate;
    [SerializeField]
    private GameObject prefabColliderAttack;

    private void Start()
    {
        // _moveTimeの時間をかけて移動するようにする
        _velocity.x = (0 - transform.position.x) / moveTime;

        _velocity.y = initForceY;
        _gravity = initForceY / moveTime;

        // 攻撃判定の初期化
        _collider = Instantiate(prefabColliderAttack, transform);
    }

    private void Update()
    {
        _velocity.y -= _gravity * 2.0f * Time.deltaTime;

        // 移動
        transform.position += _velocity * Time.deltaTime;

        if (_isStartRotate)
        {
            transform.localEulerAngles -= Vector3.forward * rotate * Mathf.Sign(_velocity.x);
        }

        // 画面外に出たら削除
        if (IsOffscreen())
        {
            Destroy(gameObject);
            Destroy(_collider);
        }

        if (_collider == null)
        {
            Destroy(gameObject);
        }
    }

    private bool IsOffscreen() { return transform.position.y < -7; }

    public override void Parried()
    {
        _velocity.x *= -1;
        _velocity.y = initForceY / 2;

        _isStartRotate = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            Destroy(_collider);
        }
    }
}
