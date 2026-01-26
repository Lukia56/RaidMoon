using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    [Header("メンバ変数")]

    [SerializeField]
    private int m_choice;
    public int Choice { get { return m_choice; } }

    private InputAction _upAction;
    private InputAction _downAction;
    private InputAction _submitAction;

    [Header("パラメータ")]

    [SerializeField]
    private int maxChoice;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip seMenu;
    [SerializeField]
    private AudioClip seConfirm;

    private void Start()
    {
        _upAction = InputSystem.actions.FindAction("UIUp");
        _downAction = InputSystem.actions.FindAction("UIDown");
        _submitAction = InputSystem.actions.FindAction("Submit");
    }

    private void Update()
    {
        if (Fader.IsFadingOut()) return;

        if (_upAction.WasPressedThisFrame())
        {
            m_choice--;

            audioSource.PlayOneShot(seMenu);
        }
        else
        if (_downAction.WasPressedThisFrame())
        {
            m_choice++;

            audioSource.PlayOneShot(seMenu);
        }

        m_choice = (m_choice + maxChoice) % maxChoice;

        if (_submitAction.WasPressedThisFrame())
        {
            audioSource.PlayOneShot(seConfirm);

            switch (m_choice)
            {
                case 0:

                    Fader.FadeToScene("MainScene");

                    break;
                case 1:

                    Fader.FadeToScene("TitleScene");

                    break;
            }
        }
    }
}
