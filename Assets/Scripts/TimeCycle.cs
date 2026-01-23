using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{

    [SerializeField]
    private float dawnStartTime;
    [SerializeField]
    private Color nightColor;
    [SerializeField]
    private Color dawnColor;

    [SerializeField]
    private RemainTime remainTime;
    [SerializeField]
    private SpriteRenderer nightBG;
    [SerializeField]
    private SpriteRenderer dawnBG;
    [SerializeField]
    private Material material;

    private void Start()
    {
        material.color = nightColor;
    }

    private void Update()
    {
        if (remainTime.TimeCounter <= dawnStartTime)
        {
            float currentCount = remainTime.LimitTime - remainTime.TimeCounter;

            float count = currentCount - (remainTime.LimitTime - dawnStartTime);
            float dawnRate = count / dawnStartTime;
            nightBG.color = new Color(1, 1, 1, 1 - dawnRate);

            Color color = Color.Lerp(nightColor, dawnColor, dawnRate);
            material.color = color;
        }
    }
}
