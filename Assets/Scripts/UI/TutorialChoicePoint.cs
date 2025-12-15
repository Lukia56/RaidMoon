using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialChoicePoint : MonoBehaviour
{
    [Header("パラメータ")]

    [SerializeField]
    private int number;                     // 自身の番号

    [SerializeField]
    private float space;                    // ドット同士の間隔
    [SerializeField]
    private float unchoiceAlpha;            // 選択されていないときのアルファ値

    [SerializeField]
    private Vector2 defaultScale;           // 選択されていないときのスケール
    [SerializeField]
    private Vector2 choicedScale;           // 選択されているときのスケール

    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Image image;

    [SerializeField]
    private TutorialController controller;

    private void Start()
    {
        rectTransform.localPosition = new Vector3(
            number * space - space * (controller.MaxChoice / 2.0f - 0.5f),
            rectTransform.localPosition.y,
            rectTransform.localPosition.z);
    }

    private void Update()
    {
        rectTransform.localScale = defaultScale;
        image.color = new Color(image.color.r, image.color.g, image.color.b, unchoiceAlpha);

        if (controller.Choice == number)
        {
            rectTransform.localScale = choicedScale;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
        }
    }
}
