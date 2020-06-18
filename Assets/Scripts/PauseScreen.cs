using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
	#region Fields

	[SerializeField] string _menuScene;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void QuitGameButtonClicked()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif

		Application.Quit();
	}

	public void MainMenuButtonClicked()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(_menuScene);
	}

	public void ResumeButtonClicked()
	{
		GameManager.Instance.PauseUnpause();
	}
	#endregion

	#region Private Methods


	#endregion
}
