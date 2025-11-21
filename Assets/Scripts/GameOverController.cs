using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    private void Update()
    {
        if (m_Player.IsDead)
        {
            m_TextMeshProUGUI.enabled = true;

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}
