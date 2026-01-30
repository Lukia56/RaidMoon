using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float scaleSpeed;

    [SerializeField]
    private float fadeSpeed;

    private void Update()
    {
        Vector3 scaleVec = Vector3.one * scaleSpeed * Time.deltaTime;
        scaleVec.x *= Mathf.Sign(transform.localScale.x);
        transform.localScale += scaleVec;

        UpdateFade();
    }

    private void UpdateFade()
    {
        // フェードさせる
        Color color = spriteRenderer.color;
        color.a -= fadeSpeed * Time.deltaTime;
        spriteRenderer.color = color;

        // 透明になったら削除
        if (spriteRenderer.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
