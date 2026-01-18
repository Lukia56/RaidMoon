using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField]
    private bool m_isFadeStart;

    [Header("ƒpƒ‰ƒ[ƒ^")]

    [SerializeField]
    private Player player;

    private void Update()
    {
        if (player.IsDead && !m_isFadeStart)
        {
            Fader.FadeToScene("GameOverScene", 5);

            m_isFadeStart = true;
        }
    }
}
