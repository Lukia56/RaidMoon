using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIArrowNum : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;
    [SerializeField] private Player m_Player;

    private void Update()
    {
        m_TextMeshProUGUI.text = m_Player.NumArrows.ToString();
    }
}
