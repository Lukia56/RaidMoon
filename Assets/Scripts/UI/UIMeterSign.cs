using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMeterSign : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private RemainTime remainTime;

    private void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.x = (1.0f - remainTime.TimeCounter / remainTime.LimitTime) * 110.0f - 55.0f;
        transform.localPosition = pos;
    }
}
