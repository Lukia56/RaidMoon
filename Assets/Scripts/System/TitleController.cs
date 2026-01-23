using UnityEngine;

public class TitleController : MonoBehaviour
{
    [Header("メンバ変数")]

	[SerializeField]
	private int m_choice;
	public int Choice { get { return m_choice; } }

	[Header("パラメータ")]

	[SerializeField]
	private int maxChoice;

	[SerializeField]
	private AudioSource audioSource;

	// メニュー選択のSE
	[SerializeField]
	private AudioClip seMenu;
	// 決定のSE
	[SerializeField]
	private AudioClip seConfirm;

	private void Start()
	{
		Application.targetFrameRate = 60;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			m_choice--;

			audioSource.PlayOneShot(seMenu);
		}
		else
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			m_choice++;

			audioSource.PlayOneShot(seMenu);
		}

		m_choice = (m_choice + maxChoice) % maxChoice;

		if (Input.GetKeyDown(KeyCode.Z))
		{
			audioSource.PlayOneShot(seConfirm);
			
			switch (m_choice)
			{
				case 0:

					Fader.FadeToScene("MainScene");

					break;

				case 1:

					Fader.FadeToScene("TutorialScene");

					break;

				case 2:

#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
#else
					Application.Quit();
#endif
					break;
			}
		}
	}
}
