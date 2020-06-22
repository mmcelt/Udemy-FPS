using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Fields

	public static GameManager Instance;

	[SerializeField] float _waitAfterDying = 2f;
	public bool _levelEnding;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		//DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseUnpause();
		}
	}
	#endregion

	#region Public Methods

	public void PlayerDied()
	{
		StartCoroutine(PlayerDiedRoutine());
	}

	public void PauseUnpause()
	{
		if (!UIController.Instance._pauseScreen.activeInHierarchy)
		{
			UIController.Instance._pauseScreen.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			//stop all non-player SFX...
			for (int i = 0; i < AudioManager.Instance._soundEfects.Length; i++)
			{
				AudioManager.Instance.StopSFX(i);
			}
			//stop player footstep SFX
			for(int i=0; i<PlayerController.Instance._footsteps.Length; i++)
			{
				PlayerController.Instance._footsteps[i].Stop();
			}
			Time.timeScale = 0;
		}
		else
		{
			UIController.Instance._pauseScreen.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1;
			//restart player footstep SFX
			for (int i = 0; i < PlayerController.Instance._footsteps.Length; i++)
			{
				PlayerController.Instance._footsteps[i].Play();
			}

		}
	}
	#endregion

	#region Private Methods

	IEnumerator PlayerDiedRoutine()
	{
		PlayerHealthController.Instance.gameObject.SetActive(false);

		yield return new WaitForSeconds(_waitAfterDying);

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	#endregion
}
