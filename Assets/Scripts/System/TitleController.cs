using UnityEngine;
using UnityEngine.InputSystem;

public class TitleController : MonoBehaviour
{
    [Header("メンバ変数")]

	[SerializeField]
	private int m_choice;
	public int Choice { get { return m_choice; } }

	private InputAction _navigateAction;
	private InputAction _upAction;
	private InputAction _downAction;
	private InputAction _submitAction;

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

		_upAction = InputSystem.actions.FindAction("UIUp");
		_downAction = InputSystem.actions.FindAction("UIDown");
		_submitAction = InputSystem.actions.FindAction("Submit");
    }

	private void Update()
	{
		if (_upAction.WasPressedThisFrame())
		{
			m_choice--;

			audioSource.PlayOneShot(seMenu);
		}
		else
		if (_downAction.WasPressedThisFrame())
		{
			m_choice++;

			audioSource.PlayOneShot(seMenu);
		}

		m_choice = (m_choice + maxChoice) % maxChoice;

		if (_submitAction.WasPressedThisFrame())
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
