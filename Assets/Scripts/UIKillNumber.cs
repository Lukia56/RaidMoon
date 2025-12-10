using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIKillNumber : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;
    [SerializeField]
    private KillNumber killNumber;

    private void Update()
    {
        textMeshPro.text = killNumber.KillNumbers.ToString();
    }
}
