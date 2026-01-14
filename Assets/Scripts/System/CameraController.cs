using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("メンバ変数")]

    [SerializeField]
    private Vector3 m_position;
    [SerializeField]
    private Vector3 m_offsetPosition;

    [SerializeField]
    private Vector3 m_shake;

    [Header("パラメータ")]

    [SerializeField]
    private float shakeDecreaseSpeed;

    private void Start()
    {
        m_position = transform.position;
    }

    private void Update()
    {
        m_shake = new Vector3(
            Mathf.MoveTowards(m_shake.x, 0.0f, shakeDecreaseSpeed * Time.deltaTime),
            Mathf.MoveTowards(m_shake.y, 0.0f, shakeDecreaseSpeed * Time.deltaTime),
            0.0f);

        m_offsetPosition = new Vector3(
            Mathf.Sin(Time.time * 1000 * Mathf.Deg2Rad) * m_shake.x,
            Mathf.Cos(Time.time * 1000 * Mathf.Deg2Rad) * m_shake.y,
            0.0f);

        transform.position = m_position + m_offsetPosition;
    }

    public void Shake(Vector3 amount)
    {
        m_shake = amount;
    }
}
