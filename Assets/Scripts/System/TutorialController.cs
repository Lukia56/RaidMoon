using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialController : MonoBehaviour
{
    [Header("メンバ変数")]

    [SerializeField]
    private int m_choice;
    public int Choice { get { return m_choice; } }

    private InputAction _leftAction;
    private InputAction _rightAction;
    private InputAction _cancelAction;

    [Header("パラメータ")]

    [SerializeField]
    private int maxChoice;
    public int MaxChoice { get { return maxChoice; } }

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

    [SerializeField]
    private AudioSource audioSource;

    // メニュー選択のSE
    [SerializeField]
    private AudioClip seMenu;

    private void Start()
    {
        _leftAction = InputSystem.actions.FindAction("UILeft");
        _rightAction = InputSystem.actions.FindAction("UIRight");
        _cancelAction = InputSystem.actions.FindAction("Cancel");
    }

    private void Update()
    {
        if (_leftAction.WasPressedThisFrame())
        {
            m_choice--;

            audioSource.PlayOneShot(seMenu);
        }
        else
        if (_rightAction.WasPressedThisFrame())
        {
            m_choice++;

            audioSource.PlayOneShot(seMenu);
        }

        if (_cancelAction.WasPressedThisFrame())
        {
            Fader.FadeToScene("TitleScene");
        }

        m_choice = (m_choice + maxChoice) % maxChoice;

        title.text = titleText[m_choice];
        description.text = descriptionText[m_choice];

        descImage.sprite = descSprites[m_choice];
    }
}
