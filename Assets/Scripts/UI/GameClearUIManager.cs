using TMPro;
using UnityEngine;

public class GameClearUIManager : MonoBehaviour
{
    [Header("パラメータ")]

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

        KillNumberText.text = "倒した敵：" + KillNumber.s_killNumbers.ToString() + "体";
    }
}
