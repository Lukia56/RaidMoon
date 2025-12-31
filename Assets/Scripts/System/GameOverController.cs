using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [Header("ƒpƒ‰ƒ[ƒ^")]

    [SerializeField]
    private Player player;

    private void Update()
    {
        if (player.IsDead)
        {
            //SceneManager.LoadScene("ResultScene");
        }
    }
}
