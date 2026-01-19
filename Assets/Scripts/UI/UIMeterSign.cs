using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMeterSign : MonoBehaviour
{
    [SerializeField]
    private float startX;

    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private RemainTime remainTime;

    private void Start()
    {
        startX = transform.localPosition.x;
    }

    private void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.x = (1.0f - remainTime.TimeCounter / remainTime.LimitTime) * (-startX * 2.0f) + startX;
        transform.localPosition = pos;
    }
}
