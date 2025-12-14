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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_choice--;
        }
        else
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_choice++;
        }

        m_choice = (m_choice + maxChoice) % maxChoice;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (m_choice)
            {
                case 0:

                    SceneManager.LoadScene("MainScene");

                    break;
                case 1:

                    SceneManager.LoadScene("TitleScene");

                    break;
            }
        }
    }
}
