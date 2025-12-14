using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearController : MonoBehaviour
{
    [Header("ƒpƒ‰ƒ[ƒ^")]

    [SerializeField]
    private RemainTime remainTime;

    private void Update()
    {
        if (remainTime.GetRemainTime() <= 0)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
