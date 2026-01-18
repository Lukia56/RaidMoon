using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    // シングルトン用インスタンス
    private static Fader s_instance;

    // フェード状態
    private enum FadeState
    {
        None,
        In,
        Out,
        Loading
    }

    [Header("メンバ変数")]

    [SerializeField]
    private FadeState m_state;      // フェード状態
    [SerializeField]
    private float m_fadeTimer;
    [SerializeField]
    private string m_nextSceneName; // 次のシーンの名前
    [SerializeField]
    private Canvas m_canvas;        // フェードに使用するカンバス
    [SerializeField]
    private Image m_image;          // フェードに使用する画像
    [SerializeField]
    private float m_fadeDuration;   // フェードにかける時間

    private void Awake()
    {
        CreateInstance();

        DontDestroyOnLoad(s_instance);

        CreateFadeCanvas();
    }

    /// <summary>
    /// インスタンスを作成する
    /// </summary>
    private void CreateInstance()
    {
        // すでにインスタンスが存在していたら削除
        if (s_instance != null)
        {
            Destroy(this);
        }

        s_instance = this;
    }

    /// <summary>
    /// フェードのカンバスを作成する
    /// </summary>
    private void CreateFadeCanvas()
    {
        // カンバスの初期設定
        m_canvas = new GameObject("FadeCanvas").AddComponent<Canvas>();
        m_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        m_canvas.sortingOrder = 1000;
        DontDestroyOnLoad(m_canvas.gameObject);

        // イメージの初期設定
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(m_canvas.transform, false);

        // コンポーネントの追加、色の設定
        m_image = imageObj.AddComponent<Image>();
        m_image.color = Color.black;

        // イメージを画面全体に広げる
        RectTransform rect = m_image.rectTransform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.one;
    }

    private void Update()
    {
        // 状態をもとに処理を分岐
        switch (m_state)
        {
            case FadeState.In:

                UpdateFadeIn();

                break;

            case FadeState.Out:

                UpdateFadeOut();

                break;

            case FadeState.Loading:

                if (SceneManager.GetActiveScene().isLoaded)
                {
                    StartFadeIn(m_fadeDuration);
                }

                break;
        }
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    private void UpdateFadeIn()
    {
        m_fadeTimer += Time.deltaTime;
        float alpha = 1.0f - (m_fadeTimer / m_fadeDuration);
        m_image.color = new Color(0, 0, 0, Mathf.Clamp01(alpha));

        // もしフェードインが終わったら
        if (m_fadeTimer >= m_fadeDuration)
        {
            m_state = FadeState.None;
            m_image.color = new Color(0, 0, 0, 0);
        }
    }

    /// <summary>
    /// フェードアウト処理
    /// </summary>
    private void UpdateFadeOut()
    {
        m_fadeTimer += Time.deltaTime;
        float alpha = (m_fadeTimer / m_fadeDuration);
        m_image.color = new Color(0, 0, 0, Mathf.Clamp01(alpha));

        // もしフェードアウトが終わったら
        if (m_fadeTimer >= m_fadeDuration)
        {
            m_state = FadeState.Loading;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(m_nextSceneName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// フェードイン処理の初期設定
    /// </summary>
    /// <param name="duration">フェードにかける時間</param>
    private void StartFadeIn(float duration)
    {
        m_fadeDuration = duration;
        m_fadeTimer = 0.0f;
        m_state = FadeState.In;
    }

    /// <summary>
    /// フェードアウト処理の初期設定
    /// </summary>
    /// <param name="sceneName">移動先のシーン名</param>
    /// <param name="duration">フェードにかける時間</param>
    private void StartFadeOut(string sceneName, float duration)
    {
        m_nextSceneName = sceneName;
        m_fadeDuration = duration;
        m_fadeTimer = 0.0f;
        m_state = FadeState.Out;
    }

    /// <summary>
    /// 他シーンへの遷移を行う
    /// </summary>
    /// <param name="sceneName">移動先のシーン名</param>
    /// <param name="duration">遷移にかける時間</param>
    public static void FadeToScene(string sceneName, float duration = 1.0f)
    {
        // まだインスタンスが生成されていなかったら新しくフェーダーを生成
        if (s_instance == null)
        {
            GameObject obj = new GameObject("SceneFader");
            s_instance = obj.AddComponent<Fader>();
        }

        s_instance.StartFadeOut(sceneName, duration);
    }

    public static bool IsFadingOut() { return s_instance.m_state == FadeState.Out; }
}
