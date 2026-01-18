using TMPro;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    [Header("ÉpÉâÉÅÅ[É^")]

    [SerializeField]
    private Color choiceColor;
    [SerializeField]
    private Color unchoiceColor;

    [SerializeField]
    private TitleController controller;

    [SerializeField]
    private TextMeshProUGUI startText;
    [SerializeField]
    private TextMeshProUGUI tutorialText;
    [SerializeField]
    private TextMeshProUGUI quitText;
    
    private void Update()
    {
        if (controller.Choice == 0)
        {
            startText.color = choiceColor;
        }
        else
        {
            startText.color = unchoiceColor;
        }

        if (controller.Choice == 1)
        {
            tutorialText.color = choiceColor;
        }
        else
        {
            tutorialText.color = unchoiceColor;
        }

        if (controller.Choice == 2)
        {
            quitText.color = choiceColor;
        }
        else
        {
            quitText.color = unchoiceColor;
        }
    }
}
