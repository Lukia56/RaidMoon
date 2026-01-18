using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _cooldownCounter;        // 敵を生成するクールダウンのカウンタ
    [SerializeField] private List<ObjectPool> _objectsPool; // オブジェクトプールのリスト

    [SerializeField] private List<EnemyStats> _statsList;   // 敵の統計のリスト
    [SerializeField] private Vector3 _generatePosition;     // 敵を生成する位置
    [SerializeField] private float _abortGenerateTime;      // 敵の生成をやめる時間
    [SerializeField] private Transform _playerTransform;    // プレイヤーのゲームオブジェクト
    [SerializeField] private Player _playerComponent;       // プレイヤーのコンポーネント
    [SerializeField] private RemainTime _remainTime;        // 残り時間を計算するコンポーネント
    [SerializeField] private KillNumber _killNumber;

    // 敵の統計
    [System.Serializable]
    public class EnemyStats
    {
        public GameObject prefab;
        public float weight;
        public float cooldown;
    };

    private void Start()
    {
        _cooldownCounter = 0;

        _objectsPool = new List<ObjectPool>();

        for (int i = 0; i < _statsList.Count; i++)
        {
            //m_ObjectsPool[i] = gameObject.AddComponent<ObjectPool>();
            //m_ObjectsPool[i].InitPoolSize = 4;
            //m_ObjectsPool[i].ObjectToPool = m_StatsList[i].prefab.GetComponent<PooledObject>();
            _objectsPool.Add(Instantiate(_statsList[i].prefab, transform).GetComponent<ObjectPool>());
        }
    }

    private void Update()
    {
        // 最初のフレームで生成すると、StackOverflowExceptionが出ることがあるため、2フレーム以降で開始
        if (Time.time <= 0) return;

        // 敵を生成できないならカウントダウン
        if (!CanGenerateEnemy())
        {
            _cooldownCounter -= Time.deltaTime;
        }
        // 敵を生成できるなら、生成する
        else
        {
            GenerateEnemy();
        }
    }

    // 敵を生成
    private void GenerateEnemy()
    {
        // 敵のリストの中からランダムに1人選ぶ
        int index = WeightedChooser.Choice(GetWeights());
        EnemyStats stats = _statsList[index];
        ObjectPool pool = _objectsPool[index];

        // 生成
        //GameObject enemy = Instantiate(stats.prefab.gameObject);
        GameObject enemy = pool.GetInstance().gameObject;

        // 方向を 1 か -1 で取得
        int direction = Random.value < 0.5f ? 1 : -1;

        // 変数を設定
        enemy.transform.position = Vector3.Scale(_generatePosition, new Vector3(direction, 1, 1));
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        enemyComponent.Direction = -direction;
        enemyComponent.PlayerTransform = _playerTransform;
        enemyComponent.PlayerComponent = _playerComponent;
        enemyComponent.KillNumbers = _killNumber;
        enemyComponent.PRemainTime = _remainTime;
        enemyComponent.PostInit();

        // クールダウンを設定
        _cooldownCounter = stats.cooldown;
    }

    // 敵のリストから重みのみ抽出
    private List<float> GetWeights()
    {
        List<float> weights = new List<float>();

        foreach (EnemyStats e in _statsList)
        {
            // リストに現在の重みを追加
            weights.Add(e.weight);
        }

        // 重みのリストを返す
        return weights;
    }

    // 敵を生成できるかどうか
    private bool CanGenerateEnemy()
    {
        // クールダウンが発生していない
        if (_cooldownCounter > 0) return false;

        // 残り時間が残っている
        if (_remainTime.GetRemainTime() <= _abortGenerateTime) return false;

        // プレイヤーが死亡していない
        if (_playerComponent.IsDead) return false;

        return true;
    }
}
