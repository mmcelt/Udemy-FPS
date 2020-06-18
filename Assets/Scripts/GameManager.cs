using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Fields

	public static GameManager Instance;

	[SerializeField] float _waitAfterDying = 2f;

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
			Time.timeScale = 0;
		}
		else
		{
			UIController.Instance._pauseScreen.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Time.timeScale = 1;

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
