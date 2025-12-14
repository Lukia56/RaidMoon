using System.Collections;
using System.Collections.Generic;
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
    private TextMeshProUGUI settingsText;
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
            settingsText.color = choiceColor;
        }
        else
        {
            settingsText.color = unchoiceColor;
        }

        if (controller.Choice == 2)
        {
            tutorialText.color = choiceColor;
        }
        else
        {
            tutorialText.color = unchoiceColor;
        }

        if (controller.Choice == 3)
        {
            quitText.color = choiceColor;
        }
        else
        {
            quitText.color = unchoiceColor;
        }
    }
}
