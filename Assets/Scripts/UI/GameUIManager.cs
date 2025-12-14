using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [Header("ÉpÉâÉÅÅ[É^")]

    [SerializeField]
    private TextMeshProUGUI remainTimeText;
    [SerializeField]
    private TextMeshProUGUI arrowNumberText;
    [SerializeField]
    private TextMeshProUGUI KillNumberText;

    [SerializeField]
    private RemainTime remainTime;
    [SerializeField]
    private KillNumber killNumber;
    [SerializeField]
    private Player player;

    private void Update()
    {
        remainTimeText.text = remainTime.GetRemainTime().ToString("F0");
        arrowNumberText.text = player.NumArrows.ToString();
        KillNumberText.text = killNumber.KillNumbers.ToString();
    }
}
