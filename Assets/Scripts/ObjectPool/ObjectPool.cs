using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int m_InitPoolSize = 4;            // 初期化時の在庫数
    public int InitPoolSize { set => m_InitPoolSize = value; }

    [SerializeField] private PooledObject m_ObjectToPool;       // 生成するインスタンスのもと
    public PooledObject ObjectToPool { set => m_ObjectToPool = value; }

    [SerializeField] private Stack<PooledObject> m_Stack;       // 在庫を管理する

    private void Awake()
    {
        Setup();
    }

    // 在庫を初期化
    private void Setup()
    {
        // 初期化
        m_Stack = new Stack<PooledObject>();

        // 生成
        for (int i = 0; i < m_InitPoolSize; i++)
        {
            PooledObject instance = Instantiate(m_ObjectToPool);
            instance.Pool = this;
            instance.gameObject.SetActive(false);

            m_Stack.Push(instance);
        }
    }

    // インスタンスを取得、生成
    public PooledObject GetInstance()
    {
        PooledObject instance = null;

        // 在庫が足らなかったら
        if (m_Stack.Count == 0)
        {
            // 新しく生成
            instance = Instantiate(m_ObjectToPool);
            instance.Pool = this;
        }
        else
        {
            // 在庫からインスタンスを取得
            instance = m_Stack.Pop();
            instance.gameObject.SetActive(true);
        }

        instance.Init();

        return instance;
    }

    // インスタンスを在庫に戻す
    public void ReturnToPool(PooledObject instance)
    {
        instance.gameObject.SetActive(false);
        m_Stack.Push(instance);
    }
}
