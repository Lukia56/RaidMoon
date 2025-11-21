using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRemainTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;
    [SerializeField] private RemainTime m_RemainTime;

    private void Update()
    {
        m_TextMeshProUGUI.text = "TIME: " + m_RemainTime.GetRemainTime().ToString() + "s";
    }
}
