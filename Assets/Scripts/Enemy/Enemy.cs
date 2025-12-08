using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PooledObject
{
    [SerializeField]
    protected int _direction;
    public int Direction { get => _direction; set => _direction = value; }

    [SerializeField]
    protected Transform _playerTransform;    // プレイヤーのトランスフォーム
    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
    [SerializeField]
    protected Player _playerComponent;       // プレイヤーのトランスフォーム
    public Player PlayerComponent { get => _playerComponent; set => _playerComponent = value; }
    [SerializeField]
    protected KillNumber _killNumbers;
    public KillNumber KillNumbers { get => _killNumbers; set => _killNumbers = value; }

    // 死亡処理
    public virtual bool Dead() { return false; }

    // はじかれ処理
    public virtual void Parried() { }
}
