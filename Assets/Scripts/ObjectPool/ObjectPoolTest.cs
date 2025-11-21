using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest : MonoBehaviour
{
    [SerializeField] ObjectPool pool;
    [SerializeField] PooledObject pooledObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pool.GetInstance();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            pooledObject.Release();
        }
    }
}
