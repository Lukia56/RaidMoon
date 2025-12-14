using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [Header("メンバ変数")]

    [SerializeField]
    private int m_choice;
    public int Choice { get { return m_choice; } }

    [Header("パラメータ")]

    [SerializeField]
    private int maxChoice;

    [SerializeField]
    // タイトルテキスト
    private List<string> titleText;

    [SerializeField]
    // 説明テキスト
    private List<string> descriptionText;

    [SerializeField]
    // 説明用画像
    private List<Sprite> descSprites;

    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private SpriteRenderer descImage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_choice--;
        }
        else
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_choice++;
        }

        m_choice = (m_choice + maxChoice) % maxChoice;

        title.text = titleText[m_choice];
        description.text = descriptionText[m_choice];

        descImage.sprite = descSprites[m_choice];
    }
}
