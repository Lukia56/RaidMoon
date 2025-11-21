using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWarp : EnemyMeleeProcess
{
    [SerializeField] EnemyMelee _enemy;

    [SerializeField] bool _isWarped;

    // ÉpÉâÉÅÅ[É^
    [SerializeField] float _warpStartRange;

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
        
        transform.position = Vector3.Scale(transform.position, new Vector3(-1, 1, 1));
       
        _enemy.Direction *= -1;

        _isWarped = true;
    }
}
