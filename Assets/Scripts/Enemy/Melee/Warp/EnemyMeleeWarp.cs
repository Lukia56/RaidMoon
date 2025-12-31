using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWarp : EnemyMeleeProcess
{
    [SerializeField] EnemyMelee _enemy;

    [SerializeField] bool _isWarped;

    // パラメータ
    [SerializeField] float _warpStartRange;

    [SerializeField]
    private GameObject afterImagePrefab;
    [SerializeField]
    private Color afterImageColor;
    [SerializeField]
    private SpriteRenderer myRenderer;

    public override void Init()
    {
        _isWarped = false;
    }

    public override void UpdateProcess()
    {

    }

    private void Update()
    {
        if (_isWarped) return;

        if (!_enemy.IsNearPosition(transform.position, _enemy.PlayerTransform.position, _warpStartRange)) return;

        // 残像を生成
        GameObject afterImage = Instantiate(afterImagePrefab, transform.position, Quaternion.identity);
        afterImage.GetComponent<SpriteRenderer>().sprite = myRenderer.sprite;
        afterImage.GetComponent<SpriteRenderer>().color = afterImageColor;
        
        // 反対に移動する
        transform.position = Vector3.Scale(transform.position, new Vector3(-1, 1, 1));
       
        // 向きを反転させる
        _enemy.Direction *= -1;

        _isWarped = true;
    }
}
