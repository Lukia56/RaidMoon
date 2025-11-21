using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainTime : MonoBehaviour
{
    [SerializeField] private float m_LimitTime;
    [SerializeField] private float m_TimeCounter = 0;

    [SerializeField] private Player m_Player;

    private void Start()
    {
        m_TimeCounter = m_LimitTime;
    }

    private void Update()
    {
        // ƒvƒŒƒCƒ„[‚ª€–S‚µ‚Ä‚¢‚È‚¢‚È‚ç
        if (m_Player.IsDead) return;

        if (m_TimeCounter < 0) return;

        m_TimeCounter -= Time.deltaTime;
    }

    public float GetRemainTime() {  return m_TimeCounter; }
}
