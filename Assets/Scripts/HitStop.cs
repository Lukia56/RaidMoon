using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [SerializeField]
    private float m_stopCounter;
    public float StopCounter { set { m_stopCounter = value; } }

    private void Update()
    {
        if (m_stopCounter > 0)
        {
            Time.timeScale = 0.1f;

            m_stopCounter -= Time.unscaledDeltaTime;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
