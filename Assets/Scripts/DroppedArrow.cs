using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DroppedArrow : MonoBehaviour
{
    [SerializeField] private float _baseY;
    [SerializeField] private float _speedX;

    [SerializeField] private float _flowRange;
    [SerializeField] private float _accelX;
    [SerializeField] private int _numArrows;
    [SerializeField] private Transform _playerTransform;
    public Transform PlayerTransform { set { _playerTransform = value; } }
    [SerializeField] private Player _playerComponent;
    public Player PlayerComponent { set { _playerComponent = value; } }

    private void Start()
    {
        _baseY = transform.position.y;
    }

    private void Update()
    {
        _speedX += _accelX;

        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, _playerTransform.position.x, _speedX * Time.deltaTime),
            _baseY + Mathf.Sin(Time.time) * _flowRange,
            transform.position.z);

        if (transform.position.x == _playerTransform.position.x)
        {
            _playerComponent.AddArrow(_numArrows);
            Destroy(gameObject);
        }
    }
}
