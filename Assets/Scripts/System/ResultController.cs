using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    [Header("メンバ変数")]

    [SerializeField]
    private int m_choice;
    public int Choice { get { return m_choice; } }

    [Header("パラメータ")]

    [SerializeField]
    private int maxChoice;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip seMenu;
    [SerializeField]
    private AudioClip seConfirm;

    private void Update()
    {
        if (Fader.IsFadingOut()) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_choice--;

            audioSource.PlayOneShot(seMenu);
        }
        else
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_choice++;

            audioSource.PlayOneShot(seMenu);
        }

        m_choice = (m_choice + maxChoice) % maxChoice;

        if (Input.GetKeyDown(KeyCode.Z))
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
