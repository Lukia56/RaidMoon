using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearController : MonoBehaviour
{
    [SerializeField] private RemainTime m_RemainTime;
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    private void Update()
    {
        if (m_RemainTime.GetRemainTime() <= 0)
        {
            m_TextMeshProUGUI.enabled = true;

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}
