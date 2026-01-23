using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeInvisible : EnemyMeleeProcess
{
    [SerializeField] EnemyMelee _enemy;
    [SerializeField] SpriteRenderer _renderer;
    //[SerializeField] ObjectPool _pool;
    [SerializeField] GameObject _afterImage;

    [SerializeField]
    private Color _color;

    [SerializeField] Vector3 _firstPosition;
    [SerializeField] private bool _isSetFirstPosition;
    [SerializeField] float _afterImageCounter;

    // ÉpÉâÉÅÅ[É^
    [SerializeField] float _showStartRange;
    [SerializeField] float _afterImageDuration;
    [SerializeField] float _afterImageAmount;

    public override void Init()
    {
        _isSetFirstPosition = false;
        _afterImageCounter = 0;

        Color color = _renderer.color;
        color.a = 1;
        _color = color;
    }

    public override void UpdateProcess()
    {
        
    }

    private void Update()
    {
        if (!_isSetFirstPosition)
        {
            _firstPosition = transform.position;
            _isSetFirstPosition = true;
        }

        SetAlpha();

        AfterImageCount();
    }

    private void SetAlpha()
    {
        bool isNear = _enemy.IsNearPosition(transform.position, _enemy.PlayerTransform.position, _showStartRange);
        if (isNear)
        {
            _renderer.color = _color;
        }
        else
        {
            bool isRunning = _enemy.State == EnemyMelee.EState.Run;

            if (isRunning)
            {
                Color color = _renderer.color;
                color.a = 0;
                _renderer.color = color;
            }
        }
    }

    private void AfterImageCount()
    {
        if (_enemy.State != EnemyMelee.EState.Run) return;

        float afterImageDistance = (Mathf.Abs(_firstPosition.x - _enemy.PlayerTransform.position.x) - _showStartRange) / (_afterImageAmount - 1);

        if (Mathf.Abs(transform.position.x) <= Mathf.Abs(_firstPosition.x) - (afterImageDistance * _afterImageCounter))
        {
            GameObject afterImage = Instantiate(_afterImage, transform.position, Quaternion.identity);
            afterImage.transform.localScale = transform.localScale;
            afterImage.GetComponent<SpriteRenderer>().sprite = _renderer.sprite;
            afterImage.GetComponent<SpriteRenderer>().color = _color;

            _afterImageCounter++;
        }
    }
}
