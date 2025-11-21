using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float _alpha;      // 透明度
    [SerializeField] private float _destroyCounter;

    [SerializeField] private float _startAlpha; // 生成時点の透明度
    [SerializeField] private float _duration;   // 残像の表示時間
    [SerializeField] private float _fadeSpeed;  // フェードアウトする速度

    private void Start()
    {
        _alpha = _startAlpha;

        _destroyCounter = _duration;
    }

    //public override void Init()
    //{
    //    _alpha = _startAlpha;

    //    _destroyCounter = _duration;
    //}

    private void Update()
    {
        // フェードアウトまでのカウント
        DestroyCount();

        // フェードアウトのカウント
        AlphaCount();
        
        // 完全に透明になったら削除
        if (_alpha <= 0)
        {
            //Release();
            Destroy(gameObject);
        }

        // 透明度を設定
        spriteRenderer.color = (spriteRenderer.color * new Color(1, 1, 1, 0)) + new Color(0, 0, 0, _alpha);
    }

    // フェードアウトまでのカウント
    private void DestroyCount()
    {
        if (_destroyCounter <= 0) return;
        
        _destroyCounter -= Time.deltaTime;
    }

    // フェードアウトのカウント
    private void AlphaCount()
    {
        if (_destroyCounter > 0) return;

        if (_alpha <= 0) return;
        
        _alpha -= _fadeSpeed * Time.deltaTime;
    }
}
