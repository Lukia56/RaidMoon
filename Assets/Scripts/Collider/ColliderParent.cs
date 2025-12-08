using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderParent : MonoBehaviour
{
    [SerializeField] private float m_LifeCounter;           // 破壊されるまでのカウンタ
    [SerializeField] private float m_HitStopTime;           // 停止する時間
    [SerializeField] private string m_TargetTagName;        // 対象のタグ名

    [SerializeField] private BoxCollider2D m_BoxCollider;   // 自身のコライダー

<<<<<<< Updated upstream
    [SerializeField] private HitStop m_HitStop;

    private void Start()
    {
        m_HitStop = GameObject.FindWithTag("HitStop").GetComponent<HitStop>();
=======
    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private Vector3 shakeAmount;

    private void Start()
    {
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
>>>>>>> Stashed changes
    }

    private void Update()
    {
        // 生存処理
        ProcessLife();
    }

    // 生存処理
    private void ProcessLife()
    {
        // まだ生存しているならカウントダウン
        if (CanLiving())
        {
            m_LifeCounter -= Time.deltaTime;
        }
        // 生存していないなら破壊
        else
        {
            Destroy(gameObject);
        }
    }

    // 生存しているか
    private bool CanLiving()
    {
        return m_LifeCounter > 0;
    }

    // 衝突処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 対象に衝突していたら
        if (collision.gameObject.tag == m_TargetTagName)
        {
            m_HitStop.StopCounter = m_HitStopTime;

            // ヒット処理
            HitToTarget(collision.gameObject);

            cameraController.Shake(shakeAmount);
        }
    }
    
    // ヒット処理
    protected virtual void HitToTarget(GameObject hitObject)
    {
        //Debug.Log("衝突しました");
    }
}
