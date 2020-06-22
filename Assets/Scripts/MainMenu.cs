using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	#region Fields

	[SerializeField] string _firstLevel;
	[SerializeField] GameObject _continueButton;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if (PlayerPrefs.HasKey("CurrentLevel"))
		{
			if(PlayerPrefs.GetString("CurrentLevel") == "")
				_continueButton.SetActive(false);
		}
		else
		{
			_continueButton.SetActive(false);
		}
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void ContinueButtonClicked()
	{
		SceneManager.LoadScene(PlayerPrefs.GetString("CurrentLevel"));
	}

	public void PlayButtonClicked()
	{
		PlayerPrefs.SetString("CurrentLevel", "");
		PlayerPrefs.SetString(_firstLevel + "_cp", "");
		SceneManager.LoadScene(_firstLevel);
	}

	public void QuitButtonClicked()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif

		Application.Quit();
	}
	#endregion

	#region Private Methods


	#endregion
}
