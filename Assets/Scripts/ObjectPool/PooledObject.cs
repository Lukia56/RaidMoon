using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;
    public ObjectPool Pool { get => _pool; set => _pool = value; }

    public virtual void Init() { }

    public void Release()
    {
        _pool.ReturnToPool(this);
    }
}
