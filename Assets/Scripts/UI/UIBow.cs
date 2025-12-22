using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBow : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Player player;

    private void Update()
    {
        image.fillAmount = player.BowChargeCounter / player.BowChargeTime;
    }
}
