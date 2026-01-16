using UnityEngine;

public class EnemyMeleeAttack : EnemyMeleeProcess
{
    [SerializeField]
    private EnemyMelee _enemy;
    [SerializeField]
    private EnemyMeleeMove _move;

    [SerializeField]
    private bool _isAttacked;           // すでに攻撃したか
    public bool IsAttacked {  get => _isAttacked; }

    [SerializeField]
    private float _delayAttackCounter;   // 攻撃開始までのカウンタ
    [SerializeField]
    private float _postAttackCounter;   // 攻撃から逃走するまでのカウンタ

    // パラメータ
    [SerializeField]
    private Vector3 _dashForce;                 // ダッシュ速度
    [SerializeField]
    private float _attackStartRange;            // 攻撃開始範囲
    [SerializeField]
    private Vector3 _offsetAttackCollider;      // 攻撃判定のオフセット
    [SerializeField]
    private GameObject _prefabAttackCollider;   // 攻撃判定のプレハブ
    [SerializeField]
    private float delayAttackTime;              // 攻撃開始までの時間
    [SerializeField]
    private float _postAttackTime;              // 攻撃から逃走するまでの時間

    // 初期化処理
    public override void Init()
    {
        _isAttacked = false;

        _delayAttackCounter = 0;
        _postAttackCounter = 0;
    }

    // 攻撃処理の更新
    public override void UpdateProcess()
    {
        Attack();

        PreAttackCounter();
        PostAttackCounter();
    }

    // 攻撃処理
    public void Attack()
    {
        // 攻撃できるなら
        if (!CanAttack()) return;

        if (_delayAttackCounter <= delayAttackTime) return;

        // 攻撃判定を生成する
        CreateAttackCollider();

        // ダッシュ
        _move.ForceMove(_dashForce);

        _isAttacked = true;
    }

    private void PreAttackCounter()
    {
        if (_isAttacked) return;

        _delayAttackCounter += Time.deltaTime;

        _move.UpdateProcess();
    }

    private void PostAttackCounter()
    {
        // 攻撃前なら
        if (!_isAttacked)
        {
            _postAttackCounter = 0;

            return;
        }

        // 攻撃後かつ、
        // 一定時間経過後、逃走開始
        _postAttackCounter += Time.deltaTime;

        if (_postAttackCounter > _postAttackTime)
        {
            if (_enemy.PlayerComponent.IsDead)
            {
                _enemy.SetState(EnemyMelee.EState.Idle);
            }
            else
            {
                _enemy.SetState(EnemyMelee.EState.Flee);
            }
        }
    }

    // 攻撃できるかどうか
    public bool CanAttack()
    {
        // 攻撃可能距離にいる
        if (!_enemy.IsNearPosition(transform.position, _enemy.PlayerTransform.position, _attackStartRange)) return false;

        // まだ攻撃していない
        if (_isAttacked) return false;

        return true;
    }

    // 攻撃判定を生成する
    private void CreateAttackCollider()
    {
        // オフセットの方向を指定する
        Vector3 offset = _offsetAttackCollider;
        offset.x *= _enemy.Direction;

        // 判定の座標を設定
        Vector3 pos = transform.position + offset;

        // 生成
        GameObject obj = Instantiate(_prefabAttackCollider, pos, Quaternion.identity);
    }
}
