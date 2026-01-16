using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainTime : MonoBehaviour
{
    static public float s_surviveTime;

    [Header("メンバ変数")]

    [SerializeField] private float m_TimeCounter;
    public float TimeCounter { get { return m_TimeCounter; } }

    [Header("パラメータ")]

    [SerializeField] private float limitTime;
    public float LimitTime { get { return limitTime; } }

    [SerializeField] private Player player;

    private void Start()
    {
        m_TimeCounter = limitTime;
    }

    private void Update()
    {
        s_surviveTime = (int)limitTime - m_TimeCounter;

        // プレイヤーが死亡していないなら
        if (player.IsDead) return;

        if (m_TimeCounter < 0) return;

        m_TimeCounter -= Time.deltaTime;
    }

    public float GetRemainTime() {  return m_TimeCounter; }
}
