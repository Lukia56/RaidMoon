using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    [Header("ÉpÉâÉÅÅ[É^")]

    [SerializeField]
    private Color choiceColor;
    [SerializeField]
    private Color unchoiceColor;

    [SerializeField]
    private ResultController controller;

    [SerializeField]
    private TextMeshProUGUI retryText;
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI surviveTimeText;
    [SerializeField]
    private TextMeshProUGUI KillNumberText;

    private void Update()
    {
        if (controller.Choice == 0)
        {
            retryText.color = choiceColor;
        }
        else
        {
            retryText.color = unchoiceColor;
        }

        if (controller.Choice == 1)
        {
            titleText.color = choiceColor;
        }
        else
        {
            titleText.color = unchoiceColor;
        }

        surviveTimeText.text = "ê∂ë∂éûä‘ÅF" + RemainTime.s_surviveTime.ToString("F2") + "ïb";
        KillNumberText.text = "ì|ÇµÇΩìGÅF" + KillNumber.s_killNumbers.ToString() + "ëÃ";
    }
}
