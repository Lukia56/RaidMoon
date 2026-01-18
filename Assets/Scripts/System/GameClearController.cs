using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearController : MonoBehaviour
{
    [SerializeField]
    private bool m_isFadeStart;

    [Header("ƒpƒ‰ƒ[ƒ^")]

    [SerializeField]
    private RemainTime remainTime;

    private void Update()
    {
        if (remainTime.GetRemainTime() <= 0 && !m_isFadeStart)
        {
            Fader.FadeToScene("GameClearScene");

            m_isFadeStart = true;
        }
    }
}
