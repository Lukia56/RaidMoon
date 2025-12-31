using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDeleter : MonoBehaviour
{
    public void OnEnd()
    {
        // アニメーションが終了したら自身を削除する
        Destroy(gameObject);
    }
}
