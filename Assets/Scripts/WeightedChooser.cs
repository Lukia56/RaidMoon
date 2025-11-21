using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyGenerator;

public static class WeightedChooser
{
    // 重み付き確率抽選
    public static int Choice(List<float> weights)
    {
        //List<float> weights = new List<float>(GetWeights());    // 重みのリストを取得
        float totalWeight = GetTotalWeight(weights);            // 重みの合計を取得

        float randomPoint = Random.Range(0, totalWeight);

        // 先頭からチェック
        float currentWeight = 0;
        for (int i = 0; i < weights.Count; i++)
        {
            // 現在までの重みの合計を取得
            currentWeight += weights[i];

            // 現在までの重みが、乱数値を上回っているか
            if (currentWeight >= randomPoint)
            {
                return i;
            }
        }

        // 乱数値が重みの合計を超えていたら、末尾を返す
        return weights.Count - 1;
    }
    
    // 重みの合計を計算
    private static float GetTotalWeight(List<float> weights)
    {
        float total = 0;
        
        foreach (var e in weights)
        {
            // 合計に現在の重みを加算
            total += e;
        }

        // リスト内の重みの合計を返す
        return total;
    }
}
