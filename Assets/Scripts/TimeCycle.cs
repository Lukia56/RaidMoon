using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{

    [SerializeField]
    private float dawnStartTime;

    [SerializeField]
    private RemainTime remainTime;
    [SerializeField]
    private SpriteRenderer nightBG;
    [SerializeField]
    private SpriteRenderer dawnBG;

    private void Update()
    {
        if (remainTime.TimeCounter <= dawnStartTime)
        {
            float currentCount = remainTime.LimitTime - remainTime.TimeCounter;

            float count = currentCount - (remainTime.LimitTime - dawnStartTime);
            float alpha = count / dawnStartTime;
            alpha = Mathf.Pow(10 * (alpha - 1), 2.0f) / 100.0f; // ExpoIn
            nightBG.color = new Color(1, 1, 1, alpha);
        }
    }
}
