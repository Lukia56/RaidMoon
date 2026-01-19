using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : Enemy
{
    [SerializeField]
    private Vector3 _velocity = Vector3.zero;
    [SerializeField]
    private float _gravity;
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

        // 移動方向に応じて向きを変える
        transform.localEulerAngles = Vector3.forward * Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg;

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

    public override bool Parried()
    {
        _velocity.x *= -1;
        _velocity.y = initForceY / 2;

        return true;
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
