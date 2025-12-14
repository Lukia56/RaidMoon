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
        m_shake = new Vector3(Mathf.MoveTowards(m_shake.x, 0.0f, shakeDecreaseSpeed), Mathf.MoveTowards(m_shake.y, 0.0f, shakeDecreaseSpeed), 0.0f);

        m_offsetPosition = new Vector3(Random.Range(-m_shake.x, m_shake.x), Random.Range(-m_shake.y, m_shake.y), 0.0f);

        transform.position = m_position + m_offsetPosition;
    }

    public void Shake(Vector3 amount)
    {
        m_shake = amount;
    }
}
